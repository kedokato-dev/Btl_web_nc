using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Btl_web_nc.Controllers
{
    public class NewsController : Controller
    {
        private readonly RssFeedService _rssFeedService;

        public NewsController(RssFeedService rssFeedService)
        {
            _rssFeedService = rssFeedService;
        }

        public IActionResult Index()
        {
            var news = _rssFeedService.GetRssFeed();
            return View(news);
        }
    }
}