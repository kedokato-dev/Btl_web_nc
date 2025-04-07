using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class ProfileRepository: IProfileRepository
    {
        private readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
          return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChangesAsync();
        }
    }
}