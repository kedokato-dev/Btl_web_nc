using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // ...existing methods...

        public async Task<IEnumerable<User>> GetUsersByNewsletterIdAsync(int newsletterId)
        {
            return await _context.Users
                .Where(u => u.Subscriptions.Any(ns => ns.NewsletterId == newsletterId))
                .ToListAsync();
        }
    }
}