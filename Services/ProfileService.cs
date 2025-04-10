using System.Collections.Generic;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<User>> GetAllProfilesAsync()
        {
            return await _profileRepository.GetAllAsync();
        }

        public async Task<User?> GetProfileByIdAsync(int id)
        {
            return await _profileRepository.GetByIdAsync(id);
        }

       
        public async Task AddProfileAsync(User user)
        {
            user.PassWord = HashPassword(user.PassWord); // Hash the password
            await _profileRepository.AddAsync(user);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateProfileAsync(User user)
        {
            var existingUser = await _profileRepository.GetByIdAsync(user.Id);
            if (existingUser != null)
            {
                var emailExists = await _profileRepository.EmailExistsAsync(user.Email);
                if (!emailExists || existingUser.Email == user.Email)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    await _profileRepository.UpdateAsync(existingUser);
                    return (true, null); // Thành công
                }
                else
                {
                    return (false, "Email đã tồn tại trong hệ thống"); // Lỗi email tồn tại
                }
            }
            return (false, "Không tìm thấy người dùng"); // Lỗi không tìm thấy người dùng
        }

        public async Task DeleteProfileAsync(int id)
        {
            await _profileRepository.DeleteAsync(id);
        }
    }
}