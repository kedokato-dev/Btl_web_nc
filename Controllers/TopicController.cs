using System.Collections.Generic;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Btl_web_nc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TopicController : Controller
    {
        private readonly TopicServices _topicServices;

        public TopicController(TopicServices topicServices)
        {
            _topicServices = topicServices;
        }

        // Hiển thị danh sách tất cả các Topic
        public async Task<IActionResult> Index()
        {
            var topics = await _topicServices.GetAllTopicsAsync();
            return View(topics);
        }

        // Hiển thị chi tiết một Topic theo ID
        public async Task<IActionResult> Details(int id)
        {
            var topic = await _topicServices.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // Hiển thị form để thêm một Topic mới
        [HttpGet]        
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý thêm một Topic mới
        [HttpPost]
        public async Task<IActionResult> Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                await _topicServices.AddTopicAsync(topic);
                return RedirectToAction(nameof(Index));
            }

            return View(topic);
        }

        // Hiển thị form để chỉnh sửa một Topic
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await _topicServices.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // Xử lý cập nhật một Topic
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _topicServices.UpdateTopicAsync(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        // Hiển thị form để xác nhận xóa một Topic
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _topicServices.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // Xử lý xóa một Topic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _topicServices.DeleteTopicAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}