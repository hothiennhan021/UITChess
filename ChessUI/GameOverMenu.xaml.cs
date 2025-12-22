using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChessLogic; // Đảm bảo đã Reference project ChessLogic

namespace ChessUI
{
    public partial class GameOverMenu : UserControl
    {
        // Sự kiện để báo ra ngoài cho MainWindow biết người dùng chọn gì
        public event Action<Option> OptionSelected;

        public GameOverMenu()
        {
            InitializeComponent();
        }

        // Hàm hiển thị thông tin (MainWindow sẽ gọi hàm này khi hết cờ)
        public void ShowGameOver(string winner, string reason)
        {
            // 1. Xử lý hiển thị Người thắng
            if (winner == "Draw")
            {
                WinnerText.Text = "HÒA CỜ";
                WinnerText.Foreground = Brushes.LightGray;
            }
            else
            {
                string winnerName = (winner == "White") ? "TRẮNG" : "ĐEN";
                WinnerText.Text = $"{winnerName} THẮNG";

                // Đổi màu chữ tiêu đề cho đẹp (Trắng -> Trắng, Đen -> Xám/Đỏ)
                WinnerText.Foreground = (winner == "White") ? Brushes.White : Brushes.Gray;
            }

            // 2. Dịch lý do sang Tiếng Việt
            // Dù server gửi "Checkmate" hay "Timeout", ta đều hiển thị tiếng Việt
            string vietnameseReason = TranslateReason(reason);
            ReasonText.Text = vietnameseReason;

            // 3. Reset trạng thái nút Chơi lại (đề phòng bị khóa từ ván trước)
            if (BtnRestart != null)
            {
                BtnRestart.IsEnabled = true;
                BtnRestart.Content = "CHƠI LẠI";
                BtnRestart.Opacity = 1.0;
            }
        }

        // Hàm phụ trợ để dịch các thuật ngữ cờ vua sang Tiếng Việt
        private string TranslateReason(string reason)
        {
            if (string.IsNullOrEmpty(reason)) return "";

            switch (reason)
            {
                case "Checkmate":
                    return "Chiếu bí";
                case "Stalemate":
                    return "Hòa pat (Hết nước đi)";
                case "Resignation":
                case "Resign":
                case "Opponent Resigned":
                    return "Đối thủ đầu hàng";
                case "Timeout":
                case "Time Out":
                    return "Hết giờ";
                case "Insufficient Material":
                    return "Không đủ quân chiếu bí";
                case "Threefold Repetition":
                    return "Lặp lại nước đi 3 lần";
                case "50-Move Rule":
                    return "Luật 50 nước đi";
                case "Draw Agreement":
                case "Mutual Agreement":
                    return "Thỏa thuận hòa";
                default:
                    // Nếu là lý do lạ hoặc đã là tiếng Việt rồi thì giữ nguyên
                    return reason;
            }
        }

        // Hàm khóa nút (được gọi khi đã bấm Chơi lại để tránh spam)
        public void DisableRestartButton()
        {
            if (BtnRestart != null)
            {
                BtnRestart.IsEnabled = false;
                BtnRestart.Content = "Đang chờ...";
                BtnRestart.Opacity = 0.5;
            }
        }

        // --- CÁC SỰ KIỆN CLICK (Gắn liền với file XAML) ---

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Analyze);
        }
    }
}