using System.Threading.Tasks;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Btl_web_nc.Controllers
{
    public class SendEmailController : Controller
    {
        private readonly ILogger<SendEmailController> _logger;
        private readonly EmailBackgroundService _emailBackgroundService;

        public SendEmailController(ILogger<SendEmailController> logger, EmailBackgroundService emailBackgroundService)
        {
            _logger = logger;
            _emailBackgroundService = emailBackgroundService;
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            ViewBag.IsRunning = _emailBackgroundService.IsRunning;
            return View();
        }

        [HttpPost("StartEmailService")]
        public IActionResult StartEmailService()
        {
            _emailBackgroundService.StartService();
            return RedirectToAction("EmailSettings");
        }

        [HttpPost("StopEmailService")]
        public IActionResult StopEmailService()
        {
            _emailBackgroundService.StopService();
            return RedirectToAction("EmailSettings");
        }
    }
}