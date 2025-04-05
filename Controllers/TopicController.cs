using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Controllers
{
    [Authorize(Roles = "Admin")] // chỉ cho admin vào thôi
    public class TopicController : Controller
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }


        public async Task<IActionResult> Index()
        {
            var topics = await _topicRepository.GetAllAsync();
            return View(topics);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                await _topicRepository.AddAsync(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Topic topic)
        {
            if (id != topic.TopicId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _topicRepository.UpdateAsync(topic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _topicRepository.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _topicRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}