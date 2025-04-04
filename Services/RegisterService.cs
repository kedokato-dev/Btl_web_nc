using System;
using System.Net.Mail;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class RegisterService
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        public async Task RegisterUser(User user)
        {
            var existingUser = await _registerRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }

            user.IsEmailConfirmed = false;
            await _registerRepository.AddUser(user);

            // Send confirmation email
            await SendConfirmationEmail(user.Email);
        }

        private async Task SendConfirmationEmail(string email)
        {
            var confirmationLink = $"https:7082/confirm-email?email={email}";
            var mailMessage = new MailMessage("no-reply@yourwebsite.com", email)
            {
                Subject = "Email Confirmation",
                Body = $"Bấm vào đây để kích hoạt tài khoản cua bạn : {confirmationLink}",
                IsBodyHtml = true
            };

            using var smtpClient = new SmtpClient("smtp.your-email-provider.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("nicknamebot03@gmail.com", "idjdkod8"),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task ConfirmEmail(string email)
        {
            await _registerRepository.ConfirmEmail(email);
        }
    }
}