using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public interface ILoginRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserById(int id);

        Task<IEnumerable<User>> GetAllUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User?> GetUserByToken(string token);
    }
}