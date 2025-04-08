using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Btl_web_nc.Repositories;
using Btl_web_nc.Services;
using Btl_web_nc.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // Lấy email từ claim
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu không có email trong claim
            }

            var subscriptions = await _newsletterRegisterServices.GetUserSubscriptionsByEmail(email); // Lấy danh sách bản tin từ dịch vụ

            // Map tuples to SubscriptionsViewModel
            var subscriptionsViewModel = subscriptions.Select(s => new SubscriptionsViewModel
            {
                UserName = s.UserName,
                NewsletterName = s.NewsletterName,
                Frequency = s.Frequency,
                UserEmail = s.UserEmail
            });

            return View(subscriptionsViewModel); // Trả về view với danh sách bản tin
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}