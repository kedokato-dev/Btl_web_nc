using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public interface IRegisterRepository
    {
        Task AddUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task ConfirmEmail(string email);
    }
}