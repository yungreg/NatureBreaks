using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;

namespace NatureBreaks.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        [HttpGet]
        public IActionResult GetAllVideos()
        {
            return Ok(_videoRepository.GetAllVideos());
        }

        // https://localhost:5001/api/video/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vid = _videoRepository.Get(id);
            if (vid == null)
            {
                return NotFound();
            }
            return Ok(vid);
        }

        // https://localhost:5001/api/video/
        [HttpPost]
        public IActionResult Post(Video video)
        {
            _videoRepository.Add(video);
            return CreatedAtAction("Get", new { id = video.Id }, video);
        }

        // https://localhost:5001/api/video/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Video vid)
        {
            if (id != vid.Id)
            {
                return BadRequest();
            }

            _videoRepository.Update(vid);
            return NoContent();
        }

        // https://localhost:5001/api/video/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _videoRepository.Delete(id);
            return NoContent();
        }
    }
}
