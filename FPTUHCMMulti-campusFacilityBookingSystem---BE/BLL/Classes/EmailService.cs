using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace BLL.Classes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationCodeAsync(string email, string code)
        {
            var emailBody = GetVerificationEmailTemplate(code);
            await SendEmailAsync(email, "Xác Thực Email - FPTU Facility Booking", emailBody);
        }

        public async Task SendPasswordResetCodeAsync(string email, string code)
        {
            var emailBody = GetPasswordResetEmailTemplate(code);
            await SendEmailAsync(email, "Đặt Lại Mật Khẩu - FPTU Facility Booking", emailBody);
        }

        private async Task SendEmailAsync(string email, string subject, string body)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port = int.Parse(_configuration["EmailSettings:Port"] ?? "587");
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"];

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(username) || 
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(senderEmail))
            {
                // Log warning: Email settings not configured
                return;
            }

            var client = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, "FPTU Facility Booking System"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }

        private string GetVerificationEmailTemplate(string code)
        {
            return $@"
<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Xác Thực Email - FPTU Facility Booking</title>
    <style>
        body {{
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .header h1 {{
            color: #0066cc;
            margin: 0;
        }}
        .content {{
            margin-bottom: 30px;
        }}
        .code {{
            font-size: 48px;
            color: #0066cc;
            letter-spacing: 8px;
            margin: 20px 0;
            font-weight: bold;
        }}
        .footer {{
            text-align: center;
            color: #666;
            font-size: 12px;
            margin-top: 30px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>FPTU Facility Booking System</h1>
        </div>
        <div class=""content"">
            <p>Cảm ơn bạn đã đăng ký sử dụng FPTU Facility Booking System.</p>
            <p>Mã xác thực email của bạn là:</p>
            <h1 class=""code"">{code}</h1>
            <p>Vui lòng nhập mã này để xác thực địa chỉ email của bạn.</p>
            <p><small>Mã này sẽ hết hạn sau <strong>24 giờ</strong>.</small></p>
        </div>
        <div class=""footer"">
            <p>&copy; 2025 FPTU Facility Booking System.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetPasswordResetEmailTemplate(string code)
        {
            return $@"
<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Đặt Lại Mật Khẩu - FPTU Facility Booking</title>
    <style>
        body {{
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .header h1 {{
            color: #dc2626;
            margin: 0;
        }}
        .content {{
            margin-bottom: 30px;
        }}
        .code {{
            font-size: 48px;
            color: #dc2626;
            letter-spacing: 8px;
            margin: 20px 0;
            font-weight: bold;
        }}
        .footer {{
            text-align: center;
            color: #666;
            font-size: 12px;
            margin-top: 30px;
        }}
        .warning {{
            background-color: #fef3c7;
            border-left: 4px solid #f59e0b;
            padding: 15px;
            margin: 20px 0;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>FPTU Facility Booking System</h1>
        </div>
        <div class=""content"">
            <h2>Yêu Cầu Đặt Lại Mật Khẩu</h2>
            <p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình.</p>
            <p>Mã đặt lại mật khẩu của bạn là:</p>
            <h1 class=""code"">{code}</h1>
            <p>Vui lòng nhập mã này để đặt lại mật khẩu của bạn.</p>
            <div class=""warning"">
                <strong>⚠️ Lưu Ý Bảo Mật:</strong><br/>
                • Mã này sẽ hết hạn sau <strong>1 giờ</strong><br/>
                • Không chia sẻ mã này với bất kỳ ai<br/>
                • Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này
            </div>
        </div>
        <div class=""footer"">
            <p>&copy; 2025 FPTU Facility Booking System.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}


