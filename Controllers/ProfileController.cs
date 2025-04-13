using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Models.ViewModels;
using Btl_web_nc.Repositories;
using Btl_web_nc.Services;
using Btl_web_nc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Btl_web_nc.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/profiles")]
        public async Task<IActionResult> GetProfiles()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            return Json(profiles);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("api/profiles/{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            return Json(profile);
        }
        // [Authorize(Roles = "User, Admin")]
        // public async Task<IActionResult> MyProfile()
        // {
        //     var userId = User.FindFirst("Id")?.Value;
        //     if (userId == null)
        //     {
        //         return Unauthorized();
        //     }

        //     // Lấy thông tin người dùng từ ProfileService
        //     var profile = await _profileService.GetProfileByIdAsync(int.Parse(userId));
        //     if (profile == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(profile);
        // }


        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> EditMyProfile()
        {
            var userId = User.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var profile = await _profileService.GetProfileByIdAsync(int.Parse(userId));
            if (profile == null)
            {
                return NotFound();
            }

            // Chuyển đổi từ User sang EditProfileViewModel
            var viewModel = new EditProfileViewModel
            {
                Id = profile.Id,
                Email = profile.Email,
                Name = profile.Name
            };

            return View(viewModel);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMyProfile(EditProfileViewModel model)
        {
            var userId = User.FindFirst("Id")?.Value;
            if (userId == null || model.Id.ToString() != userId)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                // Lấy thông tin người dùng hiện tại
                var user = await _profileService.GetProfileByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                // Cập nhật các trường cần thiết
                user.Email = model.Email;
                user.Name = model.Name;

                var result = await _profileService.UpdateProfileAsync(user);
                if (!result.Success)
                {
                    ViewBag.ErrorMessage = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("MyProfile", "Profile");
            }

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi từ ViewModel sang Model
                var user = new User
                {
                    Email = model.Email,
                    Name = model.Name,
                    PassWord = model.PassWord,
                    RoleId = model.RoleId
                    // PreferredTime = model.PreferredTime
                };

                await _profileService.AddProfileAsync(user);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                await _profileService.UpdateProfileAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            await _profileService.DeleteProfileAsync(id);
            return RedirectToAction("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


        // nhả api

        [Authorize(Roles = "Admin")]
        [HttpPost("api/profiles")]
        public async Task<IActionResult> AddProfile([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Thông tin tài khoản không hợp lệ.");
            }

            await _profileService.AddProfileAsync(user);
            return Ok(new { message = "Thêm tài khoản thành công!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("api/profiles/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("ID không khớp.");
            }

            var result = await _profileService.UpdateProfileAsync(user);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(new { message = "Cập nhật tài khoản thành công!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("api/profiles/{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            await _profileService.DeleteProfileAsync(id);

            return Ok(new { message = "Xóa tài khoản thành công!" });
        }

    }
}