using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;


        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
          return await _context.Users.ToListAsync();
        }

        public Task<User?> GetUserById(int id)
        {
            return _context.Users.FindAsync(id).AsTask();
        }

        public Task<User?> GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == username);
        }

        public Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChangesAsync();
        }

        public Task<User?> GetUserByToken(string token)
{
    return _context.Users.FirstOrDefaultAsync(u => u.Email == token);
}
    }
}