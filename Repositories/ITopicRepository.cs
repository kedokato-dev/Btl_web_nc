using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;

namespace Btl_web_nc.Repositories
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<IEnumerable<Topic>> GetHotTopics();
        Task<Topic?> GetByIdAsync(int id);
        Task AddAsync(Topic topic);
        Task UpdateAsync(Topic topic);
        Task DeleteAsync(int id);
    }
}