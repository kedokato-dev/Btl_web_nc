using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;
using Microsoft.Extensions.Configuration;

namespace Btl_web_nc.Services
{
    public class RegisterService
    {
        private readonly IRegisterRepository _registerRepository;
        private readonly IConfiguration _configuration;

        public RegisterService(IRegisterRepository registerRepository, IConfiguration configuration)
        {
            _registerRepository = registerRepository;
            _configuration = configuration;
        }

        public async Task RegisterUser(User user)
        {
            var existingUser = await _registerRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email này đã được đăng ký trước đó.");
            }

            // Set default values
            user.IsEmailConfirmed = false;
            user.CreatedAt = DateTime.Now;

            // Save user to database
            await _registerRepository.AddUser(user);

            try
            {
                // Send confirmation email
                await SendConfirmationEmail(user.Email);
                
                // Comment this line if you want email verification to be required
                // await _registerRepository.ConfirmEmail(user.Email);
            }
            catch (Exception ex)
            {
                // Log error but still allow registration
                Console.WriteLine($"Không thể gửi email xác nhận: {ex.Message}");
                
                // Auto-confirm email if sending fails
                await _registerRepository.ConfirmEmail(user.Email);
            }
        }

        // Rest of the class remains unchanged
        private async Task SendConfirmationEmail(string email)
        {
            try
            {
                // Lấy URL của ứng dụng từ cấu hình
                var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "https://localhost:7082";
                var confirmationLink = $"{baseUrl}/Register/ConfirmEmail?email={Uri.EscapeDataString(email)}";
                
                var fromEmail = _configuration["EmailSettings:Username"] ?? "your-email@gmail.com";
                var displayName = _configuration["EmailSettings:DisplayName"] ?? "Your App Name";
                
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, displayName),
                    Subject = "Xác nhận tài khoản",
                    Body = $@"
                        <html>
                        <body>
                            <p>Cảm ơn bạn đã đăng ký tài khoản!</p>
                            <p>Vui lòng nhấp vào liên kết dưới đây để kích hoạt tài khoản của bạn:</p>
                            <p><a href='{confirmationLink}'>Xác nhận tài khoản</a></p>
                        </body>
                        </html>",
                    IsBodyHtml = true
                };
                
                mailMessage.To.Add(email);
                
                using var smtpClient = new SmtpClient
                {
                    Host = _configuration["EmailSettings:SmtpHost"] ?? "smtp.gmail.com",
                    Port = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"])
                };
                
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể gửi email xác nhận: {ex.Message}", ex);
            }
        }

        public async Task ConfirmEmail(string email)
        {
            await _registerRepository.ConfirmEmail(email);
        }
    }
}