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
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {

        private TavernContext _context;

        public PlayerController(TavernContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery]int? PlayerId, [FromQuery]string PlayerName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Player> Player = from i in _context.Player select i;

            if (PlayerId != null)
            {
                Player = Player.Where(inv => inv.PlayerId == PlayerId);
            }

            if (Player == null)
            {
                return NotFound();
            }

            return Ok(Player);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetPlayer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Player player = _context.Player.Single(m => m.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Player.Add(player);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlayerExists(player.PlayerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetPlayer", new { id = player.PlayerId }, player);
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Count(e => e.PlayerId == id) > 0;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

            Player player  = _context.Player.Single(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            _context.SaveChanges();

            return Ok(player);
        }
    }
}
