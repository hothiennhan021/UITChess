using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Dùng cho FirstOrDefault

namespace MyTcpServer
{
    public static class GameManager
    {
        // Hàng đợi tìm trận ngẫu nhiên
        private static readonly List<ConnectedClient> _waitingLobby = new List<ConnectedClient>();
        private static readonly object _lobbyLock = new object();

        // Danh sách game đang active: mỗi player map tới 1 GameSession
        private static readonly ConcurrentDictionary<ConnectedClient, GameSession> _activeGames
            = new ConcurrentDictionary<ConnectedClient, GameSession>();

        // Quản lý các phòng riêng: roomId -> chủ phòng
        private static readonly ConcurrentDictionary<string, ConnectedClient> _privateRooms
            = new ConcurrentDictionary<string, ConnectedClient>();

        // Random dùng chung để tránh trùng số khi tạo phòng
        private static readonly Random _rng = new Random();
        private static readonly object _rngLock = new object();

        public static void HandleClientConnect(ConnectedClient client)
        {
            // Nếu muốn sau này có logic đánh dấu online, log, v.v thì nhét vào đây
        }

        public static void HandleClientDisconnect(ConnectedClient client)
        {
            // Xóa khỏi hàng đợi tìm trận nếu đang chờ
            lock (_lobbyLock)
            {
                _waitingLobby.Remove(client);
            }

            // Xóa phòng nếu client là chủ phòng
            var roomKey = _privateRooms.FirstOrDefault(x => x.Value == client).Key;
            if (roomKey != null)
            {
                _privateRooms.TryRemove(roomKey, out _);
            }

            // Nếu đang trong game thì xử lý thoát
            if (_activeGames.TryRemove(client, out GameSession session))
            {
                if (!session.IsGameOver())
                {
                    // Tìm người còn lại
                    var other = (session.PlayerWhite == client) ? session.PlayerBlack : session.PlayerWhite;

                    // Báo thắng cho người còn lại
                    _ = other.SendMessageAsync("GAME_OVER_FULL|Đối thủ thoát|Resigned");

                    // Xóa luôn người còn lại khỏi activeGames
                    _activeGames.TryRemove(other, out _);
                }
            }
        }

        // ================== PHÒNG RIÊNG ==================

        // Tạo phòng riêng
        public static async Task CreateRoom(ConnectedClient creator)
        {
            string id;
            lock (_rngLock)
            {
                do
                {
                    id = _rng.Next(1000, 9999).ToString();
                } while (_privateRooms.ContainsKey(id));
            }

            if (_privateRooms.TryAdd(id, creator))
            {
                await creator.SendMessageAsync($"ROOM_CREATED|{id}");
            }
            else
            {
                await creator.SendMessageAsync("ROOM_ERROR|Không thể tạo phòng.");
            }
        }

        // Vào phòng riêng
        public static async Task JoinRoom(ConnectedClient joiner, string id)
        {
            if (_privateRooms.TryRemove(id, out ConnectedClient creator))
            {
                if (!creator.Client.Connected)
                {
                    await joiner.SendMessageAsync("ROOM_ERROR|Phòng đã hủy.");
                    return;
                }

                // Tạo Game Session
                var session = new GameSession(creator, joiner);

                // Đăng ký 2 player vào danh sách game đang chơi
                _activeGames[creator] = session;
                _activeGames[joiner] = session;

                await session.StartGame();
            }
            else
            {
                await joiner.SendMessageAsync("ROOM_ERROR|Sai ID phòng.");
            }
        }

        // ================== XỬ LÝ LỆNH GAME ==================

        public static async Task ProcessGameCommand(ConnectedClient client, string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return;

            string[] parts = command.Split('|');
            string cmd = parts[0];

            // ---- 1. Lệnh FIND_GAME: chưa có game, chỉ đưa vào hàng đợi ----
            if (cmd == "FIND_GAME")
            {
                await AddToLobby(client);
                return;
            }

            // ---- 2. Các lệnh còn lại yêu cầu đang ở trong 1 game ----
            if (!_activeGames.TryGetValue(client, out GameSession session))
            {
                await client.SendMessageAsync("ERROR|Bạn chưa ở trong ván đấu nào.");
                return;
            }

            // ---- 3. Xử lý từng loại lệnh trong game ----
            if (cmd == "MOVE")
            {
                await session.HandleMove(client, command);
            }
            else if (cmd == "CHAT")
            {
                if (parts.Length > 1)
                {
                    await session.BroadcastChat(client, parts[1]);
                }
            }
            else if (cmd == "REQUEST_ANALYSIS")
            {
                await session.HandleAnalysisRequest(client);
            }
            else
            {
                // Các lệnh khác: REQUEST_RESTART, RESTART_NO, LEAVE_GAME, v.v.
                await session.HandleGameCommand(client, cmd);

                if (cmd == "LEAVE_GAME")
                {
                    _activeGames.TryRemove(session.PlayerWhite, out _);
                    _activeGames.TryRemove(session.PlayerBlack, out _);
                }
            }
        }

        // ================== MATCHMAKING NGẪU NHIÊN ==================

        private static async Task AddToLobby(ConnectedClient client)
        {
            GameSession sessionToStart = null;

            // B1: Thao tác với hàng đợi (cần lock)
            lock (_lobbyLock)
            {
                // Dọn dẹp client chết
                _waitingLobby.RemoveAll(c => !c.Client.Connected);

                if (!_waitingLobby.Contains(client))
                    _waitingLobby.Add(client);

                // Nếu đủ 2 người thì ghép cặp
                if (_waitingLobby.Count >= 2)
                {
                    var p1 = _waitingLobby[0];
                    var p2 = _waitingLobby[1];
                    _waitingLobby.RemoveRange(0, 2);

                    sessionToStart = new GameSession(p1, p2);
                    _activeGames[p1] = sessionToStart;
                    _activeGames[p2] = sessionToStart;
                }
            }
            // Kết thúc lock

            // B2: Gửi tin nhắn mạng (không nằm trong lock)
            if (sessionToStart != null)
            {
                await sessionToStart.StartGame();
            }
            else
            {
                await client.SendMessageAsync("WAITING");
            }
        }
    }
}
