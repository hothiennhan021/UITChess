using ChessData;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChessUI
{
    public partial class SelectAvatarWindow : Window
    {
        private readonly string _username;
        private readonly string _connection;

        public SelectAvatarWindow(string username, string connection)
        {
            InitializeComponent();
            _username = username;
            _connection = connection;
        }

        private async void Avatar_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var img = sender as System.Windows.Controls.Image;
                var repo = new UserRepository(_connection);

                if (img == null || img.Source == null)
                {
                    MessageBox.Show("Không thể đọc ảnh!");
                    return;
                }

                // lấy source ảnh (BitmapSource là base của BitmapFrame, BitmapImage,...)
                BitmapSource bmp = img.Source as BitmapSource;
                if (bmp == null)
                {
                    MessageBox.Show("Không đọc được dữ liệu ảnh!");
                    return;
                }

                byte[] data;

                // Encode ảnh trong memory thành PNG -> byte[]
                using (var ms = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                // lưu avatar vào DB
                await repo.UpdateAvatarAsync(_username, data);

                MessageBox.Show("Đã cập nhật avatar!", "Thành công",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn avatar:\n" + ex.Message);
            }
        }
    }
}
