using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Controllers
{
    public class NewsController : Controller
    {
        private readonly RssService _rssService;
        private readonly AppDbContext _context; // Thay thế với DbContext của bạn

        public NewsController(RssService rssService, AppDbContext context)
        {
            _rssService = rssService;
            _context = context;
        }

        public async Task<IActionResult> Index(int? newsletterId)
        {
            if (!newsletterId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            var newsletter = await _context.Newsletters.FindAsync(newsletterId);
            if (newsletter == null)
            {
                return NotFound();
            }

            // Lấy và lưu các bài viết từ RSS feed
            var articles = await _rssService.GetAndSaveFeedItemsAsync(newsletter.RssUrl, newsletterId.Value, _context);

            var viewModel = new NewsViewModel
            {
                Newsletter = newsletter,
                Articles = articles
            };

            return View(viewModel);
        }
    }
}