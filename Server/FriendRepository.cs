using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Hoặc System.Data.SqlClient tùy bạn dùng

namespace ChessData
{
    public class FriendRepository
    {
        private readonly string _connectionString;

        public FriendRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 1. Gửi lời mời kết bạn
        public string SendFriendRequest(int fromUserId, string toUsername)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // A. Tìm ID người nhận
                string findIdSql = "SELECT UserId FROM Users WHERE Username = @u";
                using (SqlCommand cmdFind = new SqlCommand(findIdSql, conn))
                {
                    cmdFind.Parameters.AddWithValue("@u", toUsername);
                    object result = cmdFind.ExecuteScalar();

                    if (result == null) return "NOT_FOUND"; // Không tìm thấy user
                    int toUserId = (int)result;

                    if (toUserId == fromUserId) return "SELF_ERROR"; // Không tự kết bạn

                    // B. Kiểm tra đã có quan hệ/bạn bè chưa
                    string checkSql = @"
                        SELECT COUNT(*) FROM Friendships 
                        WHERE ((RequesterId = @f AND ReceiverId = @t)
                            OR (RequesterId = @t AND ReceiverId = @f))";

                    using (SqlCommand cmdCheck = new SqlCommand(checkSql, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@f", fromUserId);
                        cmdCheck.Parameters.AddWithValue("@t", toUserId);

                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count > 0) return "EXISTED"; // Đã là bạn hoặc đã gửi lời mời
                    }

                    // C. Thêm lời mời kết bạn
                    string insertSql = @"
                        INSERT INTO Friendships (RequesterId, ReceiverId, Status)
                        VALUES (@f, @t, 0)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@f", fromUserId);
                        cmdInsert.Parameters.AddWithValue("@t", toUserId);
                        cmdInsert.ExecuteNonQuery();
                    }

                    return "SUCCESS";
                }
            }
        }

        // 2. Lấy danh sách bạn bè
        public List<string> GetListFriends(int userId)
        {
            List<string> list = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Lấy tên bạn bè + Elo + IsOnline
                string sql = @"
                    SELECT u.Username, u.Elo, u.IsOnline 
                    FROM Friendships f
                    JOIN Users u ON (u.UserId = f.RequesterId OR u.UserId = f.ReceiverId)
                    WHERE (f.RequesterId = @uid OR f.ReceiverId = @uid)
                      AND u.UserId != @uid
                      AND f.Status = 1";  // Chỉ lấy người đã đồng ý (Status = 1)

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Format: "Username|Elo|IsOnline"
                            list.Add($"{reader["Username"]}|{reader["Elo"]}|{reader["IsOnline"]}");
                        }
                    }
                }
            }

            return list;
        }

        // 3. Lấy danh sách lời mời kết bạn (mình là người nhận)
        public List<string> GetFriendRequests(int userId)
        {
            List<string> list = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT u.Username, f.Id AS RequestId
                    FROM Friendships f
                    JOIN Users u ON u.UserId = f.RequesterId
                    WHERE f.ReceiverId = @uid AND f.Status = 0";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Format: "RequestId|Username"
                            list.Add($"{reader["RequestId"]}|{reader["Username"]}");
                        }
                    }
                }
            }

            return list;
        }

        // 4. Đồng ý kết bạn
        public void AcceptFriend(int requestId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "UPDATE Friendships SET Status = 1 WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", requestId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 5. Xóa bạn
        public bool RemoveFriend(int myUserId, string friendName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Tìm ID của người bạn
                    string findIdSql = "SELECT UserId FROM Users WHERE Username = @u";
                    int friendId;

                    using (SqlCommand cmd = new SqlCommand(findIdSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", friendName);
                        object result = cmd.ExecuteScalar();
                        if (result == null) return false;

                        friendId = (int)result;
                    }

                    // 2. Xóa quan hệ bạn bè (cả 2 chiều)
                    string deleteSql = @"
                        DELETE FROM Friendships 
                        WHERE (RequesterId = @me AND ReceiverId = @friend)
                           OR (RequesterId = @friend AND ReceiverId = @me)";

                    using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@me", myUserId);
                        cmd.Parameters.AddWithValue("@friend", friendId);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
