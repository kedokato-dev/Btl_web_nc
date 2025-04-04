using System;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        // Hiển thị trang đăng ký
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Xử lý đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string Name, string Email, string Password, string ConfirmPassword)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || 
                string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin.");
                return View();
            }
            
            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                return View();
            }

            var user = new User
            {
                FullName = Name,
                Email = Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password), // Hash mật khẩu
                CreatedAt = DateTime.Now,
                IsEmailConfirmed = false
            };

            try
            {
                await _registerService.RegisterUser(user);
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng kiểm tra email để xác thực.";
                return RedirectToAction("Index", "Login");
            }
            catch (DbUpdateException ex)
            {
                // Handle database-specific errors
                ModelState.AddModelError("", "Lỗi cơ sở dữ liệu: " + ex.InnerException?.Message ?? ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // Xác thực email
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email không hợp lệ.");
            }

            try
            {
                await _registerService.ConfirmEmail(email);
                TempData["SuccessMessage"] = "Email đã được xác thực thành công!";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Không thể xác thực email: " + ex.Message;
                return RedirectToAction("Index", "Login");
            }
        }
    }
}