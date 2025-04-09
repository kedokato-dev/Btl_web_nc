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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<User>> GetUsersByNewsletterIdAsync(int newsletterId)
        {
            return await _context.Users
                .Where(u => u.Subscriptions.Any(ns => ns.NewsletterId == newsletterId))
                .ToListAsync();
        }
    }
}