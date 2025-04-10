using System;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Btl_web_nc.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerService;
        private readonly ProfileService _profileService;

        public RegisterController(RegisterService registerService, ProfileService profileService)
        {
            _registerService = registerService;
            _profileService = profileService;
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
        public async Task<IActionResult> Index(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi từ ViewModel sang Model
                var user = new User
                {
                    Email = model.Email,
                    Name = model.Name,
                    PassWord = BCrypt.Net.BCrypt.HashPassword(model.PassWord), // Hash mật khẩu
                    RoleId = 1, // mặc định là người dùng
                    CreatedAt = DateTime.Now,
                    IsEmailConfirmed = false,
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
                    // ModelState.AddModelError("", "Lỗi cơ sở dữ liệu: " + ex.InnerException?.Message ?? ex.Message);
                    return View();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }
            }

            return View(model);

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