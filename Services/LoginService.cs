using System.Collections.Generic;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace Btl_web_nc.Services
{
    public class LoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _loginRepository.GetAllUsers();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _loginRepository.GetUserById(id);
        }

        public async Task<User?> AuthenticateUser(string email, string password)
        {
            var user = await _loginRepository.GetUserByUsername(email);

            if (user == null)
                return null;

            // Xác thực mật khẩu sử dụng BCrypt
            bool verified = BC.Verify(password, user.PasswordHash);

            return verified ? user : null;
        }

        public static string GetRoleName(int roleId)
        {
            return roleId switch
            {
                0 => "Admin",
                1 => "User",
                2 => "Banned",
                _ => "Unknown"  // Vai trò mặc định nếu không khớp
            };
        }

    }
}