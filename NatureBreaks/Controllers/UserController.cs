using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;

namespace NatureBreaks.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        // https://localhost:5001/api/user/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // https://localhost:5001/api/user/
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _userRepository.AddUser(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        // https://localhost:5001/api/user/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // https://localhost:5001/api/user/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            _userRepository.DeleteUserById(id);
            return NoContent();
        }
    }
}
