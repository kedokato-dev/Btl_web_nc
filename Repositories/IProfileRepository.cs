using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public interface IProfileRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);

        public Task DeleteAsync(int id);
        public Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}