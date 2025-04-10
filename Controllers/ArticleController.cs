using System.Text;
using System.Threading.Tasks;
using Btl_web_nc.Models.ViewModels;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Btl_web_nc.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticlesServices _articleService;
        private readonly ICompositeViewEngine _viewEngine;

        public ArticleController(ArticlesServices articleService, ICompositeViewEngine viewEngine)
        {
            _articleService = articleService;
            _viewEngine = viewEngine;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query, int page = 1)
        {
            const int pageSize = 10;
            var allResults = await _articleService.GetArticlesAsyncByTitle(query);
            var pagedResults = allResults.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new SearchViewModel
            {
                Query = query,
                Articles = pagedResults,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)allResults.Count / pageSize)
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                try
                {
                   var articlesHtml = await RenderPartialViewToStringAsync("_ArticlesPartial", viewModel.Articles);

                    var paginationHtml = await RenderPartialViewToStringAsync("~/Views/Shared/_PaginationPartial.cshtml", viewModel);

                    return Json(new
                    {
                        success = true,
                        articlesHtml,
                        paginationHtml
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Lỗi khi render partial view: {ex.Message}",
                        stackTrace = ex.StackTrace
                    });
                }
            }

            return View(viewModel);
        }

        private async Task<string> RenderPartialViewToStringAsync(string viewPathOrName, object model)
        {
            ViewData.Model = model;

            using var writer = new StringWriter();
           var viewResult = _viewEngine.FindView(ControllerContext, viewPathOrName, false);
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

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }
    }
}
