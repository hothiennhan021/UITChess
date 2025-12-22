using System.Windows;

namespace ChessUI
{
    public partial class ChangeIngameNameDialog : Window
    {
        // Thuộc tính để lấy tên mới ra bên ngoài
        public string NewIngameName { get; private set; }

        // Constructor: Cho phép truyền tên hiện tại vào (optional)
        public ChangeIngameNameDialog(string currentName = "")
        {
            InitializeComponent();
            txtIngame.Text = currentName; // Điền sẵn tên cũ
            txtIngame.Focus();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string input = txtIngame.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Tên hiển thị không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (input.Length > 20)
            {
                MessageBox.Show("Tên quá dài (tối đa 20 ký tự).", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewIngameName = input; // Lưu tên vào biến public
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}