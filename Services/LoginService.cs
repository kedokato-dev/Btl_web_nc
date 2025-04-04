using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

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

    }



}

