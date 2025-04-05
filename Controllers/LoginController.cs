using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Btl_web_nc.Services;
using Btl_web_nc.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Btl_web_nc.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginService _loginService;

        public LoginController(ILogger<LoginController> logger, LoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            // Nếu đã đăng nhập thì chuyển hướng về trang chủ
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await _loginService.AuthenticateUser(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác");
                return View("Index", model);
            }

            if (!user.IsEmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng xác nhận email trước khi đăng nhập");
                return View("Index", model);
            }

            // Tạo claims cho người dùng
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, LoginService.GetRoleName(user.RoleId)), // ánh xạ vai trò
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe, // Cookie sẽ được lưu nếu chọn "Ghi nhớ đăng nhập"
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(7) : null
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            _logger.LogInformation($"User {user.Email} logged in at {DateTime.Now}");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}