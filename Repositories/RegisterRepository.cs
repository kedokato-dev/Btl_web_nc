using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly List<User> _users = new();

        public async Task AddUser(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Email == email));
        }

        public async Task ConfirmEmail(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.IsEmailConfirmed = true;
            }
            await Task.CompletedTask;
        }
    }
}