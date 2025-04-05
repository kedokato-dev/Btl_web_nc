using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Btl_web_nc.Component
{
    public class HotTopicsViewComponent: ViewComponent
    {
    private readonly ITopicRepository _topicRepository;

    public HotTopicsViewComponent(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var topics = await _topicRepository.GetHotTopics(); // Implement GetHotTopicsAsync in your repository
        return View(topics);
    }
}
}