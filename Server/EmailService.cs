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

        public static Task<bool> SendOtpAsync(string to, string otp)
        {
            string subject = "Mã OTP xác thực đăng ký tài khoản";
            string body = $"Mã OTP của bạn là: {otp}\nMã có hiệu lực trong 5 phút.";
            return SendEmailAsync(to, subject, body);
        }
    }
}
