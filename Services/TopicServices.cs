using System.Collections.Generic;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class TopicServices
    {
        private readonly ITopicRepository _topicRepository;

        public TopicServices(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _topicRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Topic>> GetHotTopicsAsync()
        {
            return await _topicRepository.GetHotTopicsAsync();
        }

        public async Task<Topic?> GetTopicByIdAsync(int id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }

        public async Task AddTopicAsync(Topic topic)
        {
            await _topicRepository.AddAsync(topic);
        }

        public async Task UpdateTopicAsync(Topic topic)
        {
            await _topicRepository.UpdateAsync(topic);
        }

        public async Task DeleteTopicAsync(int id)
        {
            await _topicRepository.DeleteAsync(id);
        }
    }
}