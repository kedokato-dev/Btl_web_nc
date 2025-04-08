using System.Collections.Generic;
using System.Threading.Tasks;
using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
    }
}