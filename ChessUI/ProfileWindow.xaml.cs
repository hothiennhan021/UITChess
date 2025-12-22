using ChessClient;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessUI
{
    /// <summary>
    /// Cửa sổ hiển thị thông tin chi tiết của người chơi
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly string _username;

        public ProfileWindow(string username)
        {
            InitializeComponent();
            _username = username;

            // Đặt tiêu đề cửa sổ tiếng Việt
            this.Title = "Hồ Sơ Người Chơi";

            LoadProfileFromServer();
            LoadAvatarFromServer();
        }

        // ------------------------------ TẢI THÔNG TIN (PROFILE) ------------------------------
        private async void LoadProfileFromServer()
        {
            await ClientManager.Instance.SendAsync($"GET_PROFILE|{_username}");
            string resp = ClientManager.Instance.WaitForMessage();

            if (!resp.StartsWith("PROFILE|"))
            {
                MessageBox.Show("Không thể tải thông tin người chơi!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            var p = resp.Split('|');

            // Gán dữ liệu vào giao diện
            lblIngameName.Text = p[1];
            // Lấy chữ cái đầu làm avatar mặc định
            lblAvatarLetter.Text = p[1].Substring(0, 1).ToUpper();
            lblRank.Text = p[2];
            lblHighestRank.Text = p[3];
            lblWins.Text = p[4];
            lblLosses.Text = p[5];

            // Tính tỉ lệ thắng
            int wins = int.Parse(p[4]);
            int losses = int.Parse(p[5]);
            double rate = wins + losses == 0 ? 0 : (wins * 100.0 / (wins + losses));
            lblWinRate.Text = rate.ToString("0.##") + "%";

            // Thời gian chơi
            TimeSpan t = TimeSpan.FromMinutes(int.Parse(p[6]));
            lblTotalTime.Text = t.ToString(@"hh\:mm\:ss"); // Giờ:Phút:Giây

            // Xếp hạng (Rank Title)
            int elo = int.Parse(p[2]);
            string title = GetTitleByRank(elo);
            lblTitle.Text = title;
            lblTitle.Foreground = GetTitleColor(title);
        }

        // ------------------------------ TẢI ẢNH ĐẠI DIỆN ------------------------------
        private async void LoadAvatarFromServer()
        {
            await ClientManager.Instance.SendAsync($"GET_AVATAR|{_username}");
            string resp = ClientManager.Instance.WaitForMessage();

            if (resp == "AVATAR_NULL")
            {
                AvatarBrush.ImageSource = null;
                lblAvatarLetter.Visibility = Visibility.Visible;
                return;
            }

            if (!resp.StartsWith("AVATAR|")) return;

            try
            {
                string base64 = resp.Substring(7);
                byte[] bytes = Convert.FromBase64String(base64);

                using var ms = new MemoryStream(bytes);
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = ms;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();

                AvatarBrush.ImageSource = bmp;
                lblAvatarLetter.Visibility = Visibility.Collapsed;
            }
            catch
            {
                // Nếu lỗi ảnh thì hiện chữ cái thay thế
                AvatarBrush.ImageSource = null;
                lblAvatarLetter.Visibility = Visibility.Visible;
            }
        }

        // ------------------------------ ĐỔI ẢNH ĐẠI DIỆN ------------------------------
        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ChangeAvatarDialog();
            dlg.Owner = this;

            if (dlg.ShowDialog() == true)
            {
                if (dlg.SelectedOption == "DEFAULT")
                {
                    var win = new SelectAvatarWindow(_username);
                    win.ShowDialog();
                    LoadAvatarFromServer(); // Tải lại sau khi chọn
                }
                else if (dlg.SelectedOption == "UPLOAD")
                {
                    UploadAvatarFromPC();
                }
            }
        }

        private async void UploadAvatarFromPC()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Chỉ chấp nhận file ảnh

            if (dlg.ShowDialog() != true)
                return;

            try
            {
                byte[] data = File.ReadAllBytes(dlg.FileName);

                // Kiểm tra kích thước file (ví dụ giới hạn 2MB)
                if (data.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show("Ảnh quá lớn! Vui lòng chọn ảnh dưới 2MB.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string base64 = Convert.ToBase64String(data);

                await ClientManager.Instance.SendAsync($"SET_AVATAR|{_username}|{base64}");

                string resp = ClientManager.Instance.WaitForMessage();

                if (resp == "SET_AVATAR_OK")
                    MessageBox.Show("Cập nhật ảnh đại diện thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadAvatarFromServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ảnh:\n" + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ⭐⭐⭐ ĐỔI TÊN HIỂN THỊ (INGAME NAME) ⭐⭐⭐
        private async void ChangeIngameName_Click(object sender, RoutedEventArgs e)
        {
            // Truyền tên hiện tại vào dialog (nếu dialog hỗ trợ constructor này)
            // Nếu không, dùng constructor mặc định: new ChangeIngameNameDialog()
            var dlg = new ChangeIngameNameDialog();
            dlg.Owner = this;

            if (dlg.ShowDialog() == true)
            {
                string newName = dlg.NewIngameName; // Lấy tên mới từ property

                await ClientManager.Instance.SendAsync($"SET_INGAME|{_username}|{newName}");
                string resp = ClientManager.Instance.WaitForMessage();

                if (resp == "SET_INGAME_OK")
                {
                    MessageBox.Show("Đổi tên hiển thị thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProfileFromServer(); // Tải lại để cập nhật tên mới trên giao diện
                }
                else
                {
                    MessageBox.Show("Không thể đổi tên. Có thể tên này đã tồn tại hoặc chứa ký tự cấm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // ------------------------------ HỆ THỐNG DANH HIỆU (RANK TITLE) ------------------------------
        private string GetTitleByRank(int rank)
        {
            if (rank < 1000) return "Tân Thủ";      // Beginner
            if (rank < 1200) return "Hạng Đồng";    // Bronze
            if (rank < 1500) return "Hạng Bạc";     // Silver
            if (rank < 1800) return "Hạng Vàng";    // Gold
            if (rank < 2200) return "Bạch Kim";     // Platinum
            if (rank < 2600) return "Kim Cương";    // Diamond
            return "Đại Kiện Tướng";                // Master
        }

        private SolidColorBrush GetTitleColor(string title)
        {
            return title switch
            {
                "Tân Thủ" => new SolidColorBrush(Colors.Gray),
                "Hạng Đồng" => new SolidColorBrush(Color.FromRgb(205, 127, 50)), // Màu đồng
                "Hạng Bạc" => new SolidColorBrush(Color.FromRgb(192, 192, 192)), // Màu bạc
                "Hạng Vàng" => new SolidColorBrush(Color.FromRgb(255, 215, 0)),  // Màu vàng
                "Bạch Kim" => new SolidColorBrush(Color.FromRgb(0, 255, 255)),   // Màu Cyan
                "Kim Cương" => new SolidColorBrush(Color.FromRgb(0, 255, 210)),  // Màu xanh ngọc
                "Đại Kiện Tướng" => new SolidColorBrush(Color.FromRgb(255, 0, 255)), // Màu tím
                _ => new SolidColorBrush(Colors.White),
            };
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}