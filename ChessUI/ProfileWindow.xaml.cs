using ChessData;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessUI
{
    public partial class ProfileWindow : Window
    {
        private readonly string _username;
        private readonly string _connection;

        public ProfileWindow(string username, string connectionString)
        {
            InitializeComponent();
            _username = username;
            _connection = connectionString;

            LoadProfile();
            LoadAvatarDisplay();
        }

        private async void LoadProfile()
        {
            var repo = new UserRepository(_connection);
            var stats = await repo.GetUserStatsAsync(_username);

            if (stats == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu người chơi.");
                this.Close();
                return;
            }

            lblAvatarLetter.Text = stats.IngameName.Substring(0, 1).ToUpper();
            lblIngameName.Text = stats.IngameName;
            lblRank.Text = stats.Rank.ToString();
            lblHighestRank.Text = stats.HighestRank.ToString();
            lblWins.Text = stats.Wins.ToString();
            lblLosses.Text = stats.Losses.ToString();

            double winRate = stats.Wins + stats.Losses == 0
                ? 0 : (double)stats.Wins / (stats.Wins + stats.Losses) * 100;

            lblWinRate.Text = winRate.ToString("0.##") + "%";

            TimeSpan t = TimeSpan.FromMinutes(stats.TotalPlayTimeMinutes);
            lblTotalTime.Text = t.ToString(@"hh\:mm\:ss");

            string title = GetTitleByRank(stats.Rank);
            lblTitle.Text = title;
            lblTitle.Foreground = GetTitleColor(title);
        }

        private async void LoadAvatarDisplay()
        {
            var repo = new UserRepository(_connection);
            var avatar = await repo.GetAvatarAsync(_username);

            if (avatar == null)
            {
                AvatarBrush.ImageSource = null;
                lblAvatarLetter.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                using var ms = new MemoryStream(avatar);
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = ms;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();

                AvatarBrush.ImageSource = img;
                lblAvatarLetter.Visibility = Visibility.Collapsed;
            }
            catch
            {
                AvatarBrush.ImageSource = null;
                lblAvatarLetter.Visibility = Visibility.Visible;
            }
        }

        private string GetTitleByRank(int rank)
        {
            if (rank < 1000) return "Beginner";
            if (rank < 1200) return "Bronze";
            if (rank < 1500) return "Silver";
            if (rank < 1800) return "Gold";
            if (rank < 2200) return "Platinum";
            if (rank < 2600) return "Diamond";
            return "Master";
        }

        private SolidColorBrush GetTitleColor(string title)
        {
            return title switch
            {
                "Beginner" => new SolidColorBrush(Colors.Gray),
                "Bronze" => new SolidColorBrush(Color.FromRgb(205, 127, 50)),
                "Silver" => new SolidColorBrush(Color.FromRgb(192, 192, 192)),
                "Gold" => new SolidColorBrush(Color.FromRgb(255, 215, 0)),
                "Platinum" => new SolidColorBrush(Color.FromRgb(0, 255, 255)),
                "Diamond" => new SolidColorBrush(Color.FromRgb(0, 255, 210)),
                "Master" => new SolidColorBrush(Color.FromRgb(255, 0, 255)),
                _ => new SolidColorBrush(Colors.White)
            };
        }

        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            var choose = MessageBox.Show(
                "Bạn muốn dùng Avatar mặc định hay tải từ máy?\n\n" +
                "Yes = Chọn avatar có sẵn\nNo = Tải từ máy",
                "Đổi Avatar", MessageBoxButton.YesNoCancel);

            if (choose == MessageBoxResult.Yes)
            {
                ChooseDefaultAvatar();
            }
            else if (choose == MessageBoxResult.No)
            {
                UploadAvatarFromPC();
            }
        }

        private void ChooseDefaultAvatar()
        {
            var avatarWindow = new SelectAvatarWindow(_username, _connection);
            avatarWindow.ShowDialog();
            LoadAvatarDisplay();
        }

        private async void UploadAvatarFromPC()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (dlg.ShowDialog() != true)
                return;

            try
            {
                byte[] data = File.ReadAllBytes(dlg.FileName);

                var repo = new UserRepository(_connection);
                await repo.UpdateAvatarAsync(_username, data);

                MessageBox.Show("Đã cập nhật avatar!", "Thành công",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                LoadAvatarDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật avatar:\n" + ex.Message);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
    