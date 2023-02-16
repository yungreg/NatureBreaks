using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;
using NatureBreaks.Repositories;

namespace NatureBreaks.Controllers
{
    //activate this authorize after you have checked this controller with Postman!
    //[Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteVideosController : ControllerBase
    {

        private readonly IFavoriteVideosRepository _favoriteVideoRepository;
        public FavoriteVideosController(IFavoriteVideosRepository favoriteVideoRepository)
        {
            _favoriteVideoRepository = favoriteVideoRepository;
        }

    
        // GET: api/<FavoriteVideoController>
        [HttpGet]
        public IActionResult GetAllFavorites()
        {
            return Ok(_favoriteVideoRepository.GetAllFavorites());
        }
        
        // GET api/<FavoriteVideoController>/{id}
        [HttpGet("{id}")]
        public IActionResult GetFavoriteById(int id)
        {
            var vid = _favoriteVideoRepository.GetFavoriteById(id);
            if (vid == null)
            {
                return NotFound();
            }
            return Ok(vid);
        }
        //add new Favorited video to peopel's list! important
        [HttpPost]
        public IActionResult AddFavorite(FavoriteVideos favoriteVideo)
        {
            _favoriteVideoRepository.AddFavorite(favoriteVideo);
            return CreatedAtAction("Get", new { id = favoriteVideo.Id }, favoriteVideo);
        }

        // put a favorite video in someones list. also important
        [HttpPost("{id}")]
        public IActionResult PutFavorite(int id, FavoriteVideos favoriteVideo)
        {
            if (id != favoriteVideo.Id)
            {
                return BadRequest();
            }

            _favoriteVideoRepository.AddFavorite(favoriteVideo);
            return NoContent();
        }

        // delete a favorite video
        [HttpDelete("{id}")]
        public IActionResult DeleteFavoriteById(int id)
        {
            _favoriteVideoRepository.DeleteFavoriteById(id);
            return NoContent();
        }

    }
}
