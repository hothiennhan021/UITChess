using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyTcpServer
{
    public static class EmailService
    {
        private static string senderEmail = "phamlam011106@gmail.com";
        private static string appPassword = "xxjfcwmdnenwzhun";

        // ================= GỬI MAIL CHUNG =================
        public static async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(senderEmail, appPassword)
                };

                await smtp.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gmail Error:\n" + ex.Message);
                return false;
            }
        }

        // ================= OTP ĐĂNG KÝ (GIỮ NGUYÊN) =================
        public static Task<bool> SendOtpAsync(string to, string otp)
        {
            string subject = "Mã OTP xác thực đăng ký tài khoản";
            string body =
                $"Mã OTP của bạn là: {otp}\n" +
                $"Mã có hiệu lực trong 5 phút.";

            return SendEmailAsync(to, subject, body);
        }

        // ================= OTP QUÊN MẬT KHẨU (THÊM MỚI) =================
        public static Task<bool> SendForgotPasswordOtpAsync(string to, string otp)
        {
            string subject = "Mã OTP khôi phục mật khẩu";
            string body =
                $"Bạn vừa yêu cầu khôi phục mật khẩu.\n\n" +
                $"Mã OTP của bạn là: {otp}\n" +
                $"Mã có hiệu lực trong 5 phút.\n\n" +
                $"Nếu không phải bạn, hãy bỏ qua email này.";

            return SendEmailAsync(to, subject, body);
        }
    }
}
