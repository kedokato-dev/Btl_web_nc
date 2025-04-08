using System.Text;
using System.Threading.Tasks;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class EmailNotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly SendMailServices _sendMailServices;

        public EmailNotificationService(IUserRepository userRepository, SendMailServices sendMailServices)
        {
            _userRepository = userRepository;
            _sendMailServices = sendMailServices;
        }

        public async Task SendPeriodicEmailsAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            foreach (var user in users)
            {
                var emailBody = new StringBuilder();
                emailBody.Append("<h1>Thông báo bài viết mới</h1>");
                emailBody.Append("<p>Chào bạn, đây là danh sách bài viết mới nhất:</p>");
                emailBody.Append("<ul>");
                emailBody.Append("<li>Bài viết 1</li>");
                emailBody.Append("<li>Bài viết 2</li>");
                emailBody.Append("<li>Bài viết 3</li>");
                emailBody.Append("</ul>");

                await _sendMailServices.SendEmailAsync(user.Email, "Thông báo bài viết mới", emailBody.ToString());
            }
        }
    }
}