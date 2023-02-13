using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;
using NatureBreaks.Repositories;
// For more information on enabling Web API for empty projects, visit https://

namespace NatureBreaks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NatureTypeController : ControllerBase
    {
        private readonly INatureTypeRepository _natureTypeRepository;
        public NatureTypeController(INatureTypeRepository natureTypeRepository)
        {
            _natureTypeRepository = natureTypeRepository;
        }
        // GET: api/<NatureTypeController>
        [HttpGet]
        public IActionResult GetAllNatureTypes()
        {
            return Ok(_natureTypeRepository.GetAllNatureTypes());
        }

        // GET api/<NatureTypeController>/5
        [HttpGet("{id}")]
        public IActionResult GetNatureTypeById(int id)
        {
            var type = _natureTypeRepository.GetNatureTypeById(id);
            if (type == null)
            {
                return NotFound();
            }
            return Ok(type);
        }

        // POST api/<NatureTypeController>
        [HttpPost]
        [HttpPost]
        public IActionResult AddNatureType(NatureType natureType)
        {
             _natureTypeRepository.AddNatureType(natureType);
            return CreatedAtAction("Get", new { id = natureType.Id }, natureType);
        }

        // PUT api/<NatureTypeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, NatureType natureType)
        {
                if (id != natureType.Id)
                {
                    return BadRequest();
                }

                _natureTypeRepository.UpdateNatureType(natureType);
                return NoContent();
            }
        
        // DELETE api/<NatureTypeController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNatureTypeById(int id)
        {
            _natureTypeRepository.DeleteNatureTypeById(id);
            return NoContent();
        }
    }        
    }

