using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Btl_web_nc.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicApiController : ControllerBase
    {
        private readonly TopicServices _topicServices;

        public TopicApiController(TopicServices topicServices)
        {
            _topicServices = topicServices;
        }

        // GET: api/Topic
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _topicServices.GetAllTopicsAsync();
            return Ok(topics); // Trả JSON
        }

        // GET: api/Topic/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var topic = await _topicServices.GetTopicByIdAsync(id);
            if (topic == null)
                return NotFound();

            return Ok(topic);
        }

        // POST: api/Topic
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Topic topic)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Đảm bảo `id` không được thiết lập khi thêm mới
            topic.Id = 0;

            await _topicServices.AddTopicAsync(topic);

            // Trả về đối tượng topic sau khi thêm thành công
            return Ok(new { message = "Thêm thành công" });
        }

        // PUT: api/Topic/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Topic topic)
        {
            if (id != topic.Id)
                return BadRequest("ID không khớp");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _topicServices.GetTopicByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Gán giá trị từ entity client vào entity đang được tracking
            existing.Name = topic.Name;
            existing.IsActive = topic.IsActive;

            await _topicServices.UpdateTopicAsync(existing);

            return Ok(new { success = true, message = "Cập nhật thành công" });
        }


        // DELETE: api/Topic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _topicServices.GetTopicByIdAsync(id);
            if (topic == null)
                return NotFound();

            await _topicServices.DeleteTopicAsync(id);
            return Ok(new { success = true, message = "Xóa thành công" });
        }
    }
}
