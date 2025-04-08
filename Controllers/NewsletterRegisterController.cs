using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Btl_web_nc.Controllers
{
    [Route("[controller]")]
    public class NewsletterRegisterController : Controller
    {
        private readonly NewsletterRegisterServices _newsletterRegisterServices;

        public NewsletterRegisterController(NewsletterRegisterServices newsletterRegisterServices)
        {
            _newsletterRegisterServices = newsletterRegisterServices;
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
            return View(); // Hiển thị form đăng ký nhận tin
        }

        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe(Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return View(subscription); // Trả về view nếu dữ liệu không hợp lệ
            }

            await _newsletterRegisterServices.AddSubscription(subscription);
            return RedirectToAction("Index"); // Chuyển hướng về danh sách đăng ký
        }

        [HttpGet("Unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            var subscription = await _newsletterRegisterServices.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy đăng ký
            }

            return View(subscription); // Hiển thị xác nhận hủy đăng ký
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
            if (!ModelState.IsValid)
            {
                return View(subscription); // Trả về view nếu dữ liệu không hợp lệ
            }

            await _newsletterRegisterServices.UpdateSubscription(subscription);
            return RedirectToAction("Index"); // Chuyển hướng về danh sách đăng ký
        }
    }
}