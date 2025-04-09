using System.Text;
using System.Threading.Tasks;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class EmailNotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly INewsletterRepository _newsletterRepository;
        private readonly SendMailServices _sendMailServices;

        public EmailNotificationService(
            IUserRepository userRepository,
            INewsletterRepository newsletterRepository,
            SendMailServices sendMailServices)
        {
            _userRepository = userRepository;
            _newsletterRepository = newsletterRepository;
            _sendMailServices = sendMailServices;
        }

        public async Task SendEmailsForNewsletterAsync()
        {
            var newsletters = await _newsletterRepository.GetNewsletters();

            foreach (var newsletter in newsletters)
            {
                var users = await _userRepository.GetUsersByNewsletterIdAsync(newsletter.Id);

                foreach (var user in users)
                {
                    var emailBody = new StringBuilder();
                    emailBody.Append("<h1>Thông báo bài viết mới từ NguoiTrongNganh.com</h1>");
                    emailBody.Append($"<p>Chào {user.Name}, đây là danh sách bài viết mới nhất từ {newsletter.Name}:</p>");
                    emailBody.Append("<ul>");

                    var articles = await _newsletterRepository.GetLatestArticlesByNewsletterIdAsync(newsletter.Id);
                    foreach (var article in articles)
                    {
                        emailBody.Append($"<li>{article.Title}</li>");
                        emailBody.Append($"<p>{article.Summary}</p>");
                        emailBody.Append($"<a href='{article.Link}'>Xem bài viết</a>");
                    }
                    emailBody.Append("</ul>");
                    emailBody.Append("<p>Cảm ơn bạn đã theo dõi!</p>");
                    emailBody.Append("</ul>");

                    await _sendMailServices.SendEmailAsync(user.Email, $"Thông báo từ {newsletter.Name}", emailBody.ToString());
                }
            }
        }
    }
}