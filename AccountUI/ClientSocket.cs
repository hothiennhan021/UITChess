using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace AccountUI
{
    public static class ClientSocket
    {
        private static TcpClient _client;
        private static NetworkStream _stream;
        private static StreamReader _reader;

        private const string SERVER_IP = "20.2.251.78";
        private const int PORT = 8888;

        public static bool Connect(string ipAddress = SERVER_IP, int port = PORT)
        {
            try
            {
                if (_client != null && _client.Connected)
                    return true;

                _client = new TcpClient();
                var result = _client.BeginConnect(ipAddress, port, null, null);
                if (!result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3)))
                    return false;

                _client.EndConnect(result);
                _stream = _client.GetStream();

                // 🔥 THÊM reader để đọc theo dòng
                _reader = new StreamReader(_stream, Encoding.UTF8, false, 1024, true);

                return true;
            }
            catch
            {
                _client = null;
                _stream = null;
                _reader = null;
                return false;
            }
        }

        public static string SendAndReceive(string message)
        {
            if (_client == null || !_client.Connected)
            {
                if (!Connect())
                    return "ERROR|Mất kết nối Server";
            }

            try
            {
                // GỬI
                byte[] dataToSend = Encoding.UTF8.GetBytes(message + "\n");
                _stream.Write(dataToSend, 0, dataToSend.Length);
                _stream.Flush();

                // 🔥 ĐỌC THEO DÒNG (QUAN TRỌNG NHẤT)
                string response = _reader.ReadLine();

                return response?.Trim() ?? "";
            }
            catch (Exception ex)
            {
                // ❌ KHÔNG tự Disconnect ở đây
                return "ERROR|Lỗi mạng: " + ex.Message;
            }
        }

        public static void Disconnect()
        {
            try
            {
                _reader?.Close();
                _stream?.Close();
                _client?.Close();
            }
            catch { }

            _reader = null;
            _stream = null;
            _client = null;
        }
    }
}
