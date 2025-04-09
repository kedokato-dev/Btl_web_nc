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
            await _profileRepository.AddAsync(user);
        }

        public async Task UpdateProfileAsync(User user)
        {
            var existingUser = await _profileRepository.GetByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                await _profileRepository.UpdateAsync(existingUser);
            }
        }

        public async Task DeleteProfileAsync(int id)
        {
            await _profileRepository.DeleteAsync(id);
        }
    }
}