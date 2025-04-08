using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Models.ViewModels;
using Btl_web_nc.Repositories;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Btl_web_nc.Controllers
{
    public class NewsController : Controller
    {
        private readonly RssService _rssService;
        private readonly ArticleRepository _articleRepository;
        private readonly ICompositeViewEngine _viewEngine; // Khai báo _viewEngine

        public NewsController(RssService rssService, ArticleRepository articleRepository, ICompositeViewEngine viewEngine)
        {
            _rssService = rssService;
            _articleRepository = articleRepository;
            _viewEngine = viewEngine; // Khởi tạo _viewEngine
        }

        public async Task<IActionResult> Index(int? newsletterId, int page = 1)
        {
            if (!newsletterId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            // Lấy thông tin Newsletter
            var newsletter = await _articleRepository.GetNewsletterByIdAsync(newsletterId.Value);
            if (newsletter == null)
            {
                return NotFound();
            }

            // Lưu các bài viết từ RSS feed
            await _rssService.SaveFeedItemsFromRssAsync(newsletter.RssUrl, newsletterId.Value);

            // Lấy các bài viết với phân trang
            var (articles, totalPages) = await _rssService.GetPagedArticlesByNewsletterIdAsync(newsletterId.Value, page);

            var viewModel = new NewsViewModel
            {
                Newsletter = newsletter,
                Articles = articles,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedArticles(int newsletterId, int page)
        {
            try
            {
                var (articles, totalPages) = await _rssService.GetPagedArticlesByNewsletterIdAsync(newsletterId, page);

                // Sử dụng đường dẫn đầy đủ cho partial views
                var articlesHtml = RenderPartialViewToString("~/Views/Shared/Partials/_ArticlesPartial.cshtml", articles);
                var paginationHtml = RenderPartialViewToString("~/Views/Shared/Partials/_PaginationPartial.cshtml", new PaginationViewModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages
                });

                return Json(new { success = true, articlesHtml, paginationHtml });
            }
            catch (Exception ex)
            {
                // Trả về lỗi chi tiết hơn để debug
                return Json(new
                {
                    success = false,
                    message = $"Lỗi khi tải bài viết: {ex.Message}",
                    stackTrace = ex.StackTrace
                });
            }
        }

        private string RenderPartialViewToString(string viewPathOrName, object model)
        {
            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult;

                // Nếu là đường dẫn tuyệt đối (bắt đầu bằng ~ hoặc /) thì dùng GetView
                if (viewPathOrName.StartsWith("~") || viewPathOrName.StartsWith("/"))
                {
                    viewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewPathOrName, isMainPage: false);
                }
                else
                {
                    // Ngược lại, chỉ truyền tên thì dùng FindView
                    viewResult = _viewEngine.FindView(ControllerContext, viewPathOrName, false);
                }

                if (!viewResult.Success)
                {
                    throw new InvalidOperationException($"Không tìm thấy view '{viewPathOrName}'.");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext).Wait();
                return writer.GetStringBuilder().ToString();
            }
        }
    }

}