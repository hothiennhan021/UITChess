using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AccountUI
{
    public partial class Recovery : Form
    {
        public static string selectedEmail = "";

        public Recovery()
        {
            InitializeComponent();
            Load += Recovery_Load;
            Resize += Recovery_Resize;
        }

        private void Recovery_Load(object sender, EventArgs e)
        {
            CenterCard();
            MakeRounded(panelCard, 26);
            MakeRounded(btnGui, 20);
            MakeRounded(btnXacNhan, 20);
            MakeRounded(btnQuayLai, 18);
        }

        private void Recovery_Resize(object sender, EventArgs e)
        {
            CenterCard();
        }

        private void CenterCard()
        {
            panelCard.Left = (ClientSize.Width - panelCard.Width) / 2;
            panelCard.Top = (ClientSize.Height - panelCard.Height) / 2;
        }

        private void MakeRounded(Control c, int radius)
        {
            Rectangle rect = new Rectangle(0, 0, c.Width, c.Height);
            using GraphicsPath path = RoundedRect(rect, radius);
            c.Region = new Region(path);
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ================= QUÊN MẬT KHẨU =================

        private void btnGui_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (email == "")
            {
                MessageBox.Show("Vui lòng nhập Email!");
                return;
            }

            string res = ClientSocket.SendAndReceive($"FORGOT_SEND_OTP|{email}");

            if (string.IsNullOrWhiteSpace(res))
            {
                MessageBox.Show("Không nhận được phản hồi từ server!");
                return;
            }

            if (res.StartsWith("OK"))
            {
                selectedEmail = email;
                MessageBox.Show("Đã gửi OTP, hãy kiểm tra email!");
            }
            else if (res.StartsWith("ERROR|"))
            {
                MessageBox.Show(res.Substring(6));
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại!");
            }


        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string otp = txtOTP.Text.Trim();

            string res = ClientSocket.SendAndReceive(
                $"FORGOT_VERIFY_OTP|{selectedEmail}|{otp}");

            if (res == "OK")
            {
                MessageBox.Show("OTP chính xác!");
                new Resetpassword().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show(res);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
