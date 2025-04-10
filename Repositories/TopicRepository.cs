using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetHotTopicsAsync()
        {
            return await _context.Topics.Where(t => (bool)t.IsActive).ToListAsync();
        }

        public async Task<Topic?> GetByIdAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task AddAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Topic topic)
        {
            // _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }
    }
}