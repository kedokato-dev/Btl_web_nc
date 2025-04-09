using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public interface INewsletterRepository
    {
        Task<IEnumerable<Newsletter>> GetNewsletters();
        Task<IEnumerable<Newsletter>> GetNewslettersByTopicId(int topicId);

    }
}