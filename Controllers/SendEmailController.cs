using Microsoft.AspNetCore.Mvc;
using Btl_web_nc.Services;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Btl_web_nc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SendEmailController : Controller
    {
        private readonly EmailBackgroundService _emailBackgroundService;
        private static TimeSpan _sendTime = new TimeSpan(2, 06, 0); // Giờ gửi mặc định: 8:00 sáng

        public SendEmailController(EmailBackgroundService emailBackgroundService)
        {
            _emailBackgroundService = emailBackgroundService;
        }

        [HttpPost]
        public IActionResult StartEmailService()
        {
            if (!_emailBackgroundService.IsRunning)
            {
                _emailBackgroundService.StartService();
            }

            return RedirectToAction("Index", "SendEmail");
        }

        [HttpPost]
        public IActionResult StopEmailService()
        {
            if (_emailBackgroundService.IsRunning)
            {
                _emailBackgroundService.StopService();
            }

            return RedirectToAction("Index", "SendEmail");
        }

        [HttpPost]
        public IActionResult SetSendTime(string sendTime)
        {
            if (TimeSpan.TryParse(sendTime, out var parsedTime))
            {
                _sendTime = parsedTime;
                EmailBackgroundService.UpdateSendTime(parsedTime); // Cập nhật giờ gửi cho EmailBackgroundService
                TempData["Message"] = $"Giờ gửi email đã được cập nhật thành {_sendTime.Hours:D2}:{_sendTime.Minutes:D2}.";
            }
            else
            {
                TempData["Error"] = "Giờ gửi không hợp lệ.";
            }

            return RedirectToAction("Index", "SendEmail");
        }

        public IActionResult Index()
        {
            ViewBag.IsRunning = _emailBackgroundService.IsRunning;
            ViewBag.SendTime = _sendTime;
            return View("EmailSettings");
        }
    }
}