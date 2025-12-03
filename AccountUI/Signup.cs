using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AccountUI
{
    public partial class Signup : Form
    {
        private bool _otpVerified = false;

        public Signup()
        {
            InitializeComponent();

            btnSendOtp.Click += btnSendOtp_Click;
            btnVerifyOtp.Click += btnVerifyOtp_Click;
        }

        // =======================
        //         ĐĂNG KÝ
        // =======================
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string email = txtEmail.Text;
            string pass = textBox3.Text;
            string pass2 = textBox4.Text;
            string fullName = txtFullName.Text;
            string birthday = dtpBirthday.Value.ToString("yyyy-MM-dd");

            // ==== CHECK TÀI KHOẢN ====
            if (!Regex.IsMatch(username, @"^[A-Za-z0-9]{6,24}$") || username == "Tên Đăng Nhập")
            {
                MessageBox.Show("Tên tài khoản phải 6-24 ký tự không dấu!", "Lỗi");
                return;
            }

            // ==== CHECK EMAIL (mới) ====
            if (!Regex.IsMatch(email, @"^[^@\s]+@gmail\.com(\.vn)?$") || email == "Email")
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi");
                return;
            }

            // ==== CHECK HỌ TÊN ====
            if (fullName == "" || fullName == "Họ và Tên")
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi");
                return;
            }

            // ==== CHECK MẬT KHẨU ====
            if (!Regex.IsMatch(pass, @"^[A-Za-z0-9]{6,24}$") || pass == "Mật Khẩu")
            {
                MessageBox.Show("Mật khẩu phải 6-24 ký tự!", "Lỗi");
                return;
            }

            if (pass != pass2)
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng!", "Lỗi");
                return;
            }

            // ==== OTP CHƯA XÁC MINH ====
            if (!_otpVerified)
            {
                MessageBox.Show("Bạn phải xác minh OTP trước khi đăng ký!", "Chú ý");
                return;
            }

            try
            {
                string req = $"REGISTER|{username}|{pass}|{email}|{fullName}|{birthday}";
                string res = ClientSocket.SendAndReceive(req);

                if (string.IsNullOrWhiteSpace(res))
                {
                    MessageBox.Show("Server không phản hồi!", "Lỗi");
                    return;
                }

                string[] p = res.Split('|');

                if (p[0] == "REGISTER_SUCCESS")
                {
                    MessageBox.Show("Đăng ký thành công!", "OK");
                    this.Close();
                }
                else if (p[0] == "ERROR")
                {
                    MessageBox.Show(p.Length > 1 ? p[1] : "Lỗi đăng ký.", "Lỗi");
                }
                else
                {
                    MessageBox.Show("Lỗi phản hồi từ server: " + res);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối server: " + ex.Message);
            }
        }

        // =======================
        //         GỬI OTP
        // =======================
        private void btnSendOtp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (!Regex.IsMatch(email, @"^[^@\s]+@gmail\.com(\.vn)?$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi");
                return;
            }

            string res = ClientSocket.SendAndReceive($"REQUEST_OTP|{email}");

            if (res.StartsWith("OTP_SENT"))
            {
                MessageBox.Show("OTP đã được gửi vào email!", "Thành công");
                _otpVerified = false;
            }
            else if (res.StartsWith("ERROR"))
            {
                string[] p = res.Split('|');
                MessageBox.Show(p.Length > 1 ? p[1] : "Lỗi gửi OTP");
            }
            else
            {
                MessageBox.Show("Lỗi phản hồi từ server: " + res);
            }
        }

        // =======================
        //        XÁC MINH OTP
        // =======================
        private void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string otp = txtOtp.Text.Trim();

            if (otp == "" || otp == "Nhập mã OTP")
            {
                MessageBox.Show("Bạn chưa nhập OTP!", "Lỗi");
                return;
            }

            string res = ClientSocket.SendAndReceive($"VERIFY_OTP|{email}|{otp}");

            if (res.StartsWith("OTP_OK"))
            {
                MessageBox.Show("Xác minh OTP thành công!", "OK");
                _otpVerified = true;
            }
            else if (res.StartsWith("ERROR"))
            {
                string[] p = res.Split('|');
                MessageBox.Show(p.Length > 1 ? p[1] : "OTP sai hoặc hết hạn!", "Lỗi");
                _otpVerified = false;
            }
            else
            {
                MessageBox.Show("Phản hồi không hợp lệ: " + res);
            }
        }

        // =======================
        //     CODE CŨ (GIỮ NGUYÊN)
        // =======================

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quay lại?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Tên Đăng Nhập")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.DarkSlateBlue;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Tên Đăng Nhập";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.DarkSlateBlue;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Color.Gray;
            }
        }

        private void TxtFullName_Enter(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Họ và Tên")
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = Color.DarkSlateBlue;
            }
        }

        private void TxtFullName_Leave(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")
            {
                txtFullName.Text = "Họ và Tên";
                txtFullName.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Mật Khẩu")
            {
                textBox3.Text = "";
                textBox3.PasswordChar = '*';
                textBox3.ForeColor = Color.DarkSlateBlue;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Mật Khẩu";
                textBox3.PasswordChar = '\0';
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Xác Nhận Mật Khẩu")
            {
                textBox4.Text = "";
                textBox4.PasswordChar = '*';
                textBox4.ForeColor = Color.DarkSlateBlue;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Xác Nhận Mật Khẩu";
                textBox4.PasswordChar = '\0';
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void button_passwordhide_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '\0')
            {
                button_passwordshow.BringToFront();
                textBox3.PasswordChar = '*';
            }
        }

        private void button_passwordshow_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                button_passwordhide.BringToFront();
                textBox3.PasswordChar = '\0';
            }
        }

        private void button_passwordhide2_Click(object sender, EventArgs e)
        {
            if (textBox4.PasswordChar == '\0')
            {
                button_passwordshow2.BringToFront();
                textBox4.PasswordChar = '*';
            }
        }

        private void button_passwordshow2_Click(object sender, EventArgs e)
        {
            if (textBox4.PasswordChar == '*')
            {
                button_passwordhide2.BringToFront();
                textBox4.PasswordChar = '\0';
            }
        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void txtOtp_Enter(object sender, EventArgs e)
        {
            if (txtOtp.Text == "Nhập mã OTP")
            {
                txtOtp.Text = "";
                txtOtp.ForeColor = Color.DarkSlateBlue;
            }
        }

        private void txtOtp_Leave(object sender, EventArgs e)
        {
            if (txtOtp.Text == "")
            {
                txtOtp.Text = "Nhập mã OTP";
                txtOtp.ForeColor = Color.Gray;
            }
        }
    }
}
