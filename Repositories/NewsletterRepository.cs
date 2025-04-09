using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private readonly AppDbContext _context;
        public NewsletterRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Newsletter>> GetNewsletters()
        {
            return await _context.Newsletters.ToListAsync();
        }

        public async Task<IEnumerable<Newsletter>> GetNewslettersByTopicId(int topicId)
        {
          return await _context.Newsletters
                .Where(n => n.TopicId == topicId)
                .ToListAsync();
        }
    }
}