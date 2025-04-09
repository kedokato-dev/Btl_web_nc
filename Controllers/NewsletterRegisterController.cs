using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Controllers
{
    [Route("[controller]")]
    public class NewsletterRegisterController : Controller
    {
        private readonly NewsletterRegisterServices _newsletterRegisterServices;
        private readonly TopicServices _topicServices;
        private readonly AppDbContext _context;

        public NewsletterRegisterController(NewsletterRegisterServices newsletterRegisterServices, AppDbContext context)
        {
            _newsletterRegisterServices = newsletterRegisterServices;
             _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // Lấy email từ claim
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu không có email trong claim
            }

            var subscriptions = await _newsletterRegisterServices.GetUserSubscriptionsByEmail(email);
            return View(subscriptions); // Trả về view với danh sách bản tin
        }

        [HttpGet("Subscribe")]
        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe(Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                subscription.CreatedAt = DateTime.Now;
                subscription.LastSentAt = null;
                await _newsletterRegisterServices.AddSubscription(subscription);
                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("Index");
            }

            // Ghi log lỗi nếu dữ liệu không hợp lệ
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }

            return View(subscription);
        }

        [HttpGet("Unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
             var subscription = await _context.Subscriptions
            .Include(s => s.Newsletter) // Nối bảng Newsletter
            .FirstOrDefaultAsync(s => s.Id == id);

            // var subscription = await _newsletterRegisterServices.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        [HttpPost("Unsubscribe/{id}")]
        public async Task<IActionResult> ConfirmUnsubscribe(int id)
        {
            await _newsletterRegisterServices.DeleteSubscription(id);
            return RedirectToAction("Index"); // Chuyển hướng về danh sách đăng ký
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var subscription = await _newsletterRegisterServices.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy đăng ký
            }

            return View(subscription); // Hiển thị form chỉnh sửa đăng ký
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                return View(subscription); // Trả về view nếu dữ liệu không hợp lệ
            }

            await _newsletterRegisterServices.UpdateSubscription(subscription);
            return RedirectToAction("Index"); // Chuyển hướng về danh sách đăng ký
        }

    }


}