using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class NewsletterServices
    {
        private readonly INewsletterRepository _newsletterRepository;
        public NewsletterServices(INewsletterRepository newsletterRepository)
        {
            _newsletterRepository = newsletterRepository;
        }
        public async Task<IEnumerable<Newsletter>> GetNewsletters()
        {
            return await _newsletterRepository.GetNewsletters();
        }
        public async Task<IEnumerable<Newsletter>> GetNewslettersByTopicId(int topicId)
        {
            return await _newsletterRepository.GetNewslettersByTopicId(topicId);
        }
    }
}