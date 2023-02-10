using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NatureBreaks.Controllers
{
    //activate thi sauthorize
    //[Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteVideoController : ControllerBase
    {
        // GET: api/<FavoriteVideoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FavoriteVideoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FavoriteVideoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FavoriteVideoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FavoriteVideoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
