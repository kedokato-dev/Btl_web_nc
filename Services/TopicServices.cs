using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<IEnumerable<Topic>> GetAllProducts()
    {
        return await _topicRepository.GetAllAsync();
    }
    }
}
