using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TavernAPI.Models;


namespace TavernAPI.Controllers
{

    [Produces("application/json")]
    [EnableCors("AllowDevEnvironment")]
    [Route("api/PlayerID/[controller]")]
    public class CharacterController : Controller
    {
        private TavernContext _context;

        public CharacterController(TavernContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get([FromQuery]int? CharacterId, [FromBody]int playerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Character> Character = from i in _context.Character
                                              select i;

            if (CharacterId != null)
            {
                Character = Character.Where(inv => inv.CharacterId == CharacterId);
            }

            if (Character == null)
            {
                return NotFound();
            }

            return Ok(Character);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCharacter")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Character character = _context.Character.Single(m => m.CharacterId == id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Character.Add(character);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CharacterExists(character.CharacterId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCharacter", new { id = character.CharacterId }, character);
        }

        private bool CharacterExists(int id)
        {
            return _context.Character.Count(e => e.CharacterId == id) > 0;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.CharacterId)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Character character = _context.Character.Single(m => m.CharacterId == id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Character.Remove(character);
            _context.SaveChanges();

            return Ok(character);
        }
    }
}
