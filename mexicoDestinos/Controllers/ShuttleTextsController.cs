using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mexicoDestinos.Contexts;
using mexicoDestinos.Models;

namespace mexicoDestinos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShuttleTextsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShuttleTextsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ShuttleTexts
        [HttpGet]
        public IEnumerable<ShuttleText> GetShuttlesText()
        {
            return _context.ShuttleTexts;
        }

        // GET: api/ShuttleTexts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShuttleText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shuttleText = await _context.ShuttleTexts.FindAsync(id);

            if (shuttleText == null)
            {
                return NotFound();
            }

            return Ok(shuttleText);
        }

        // PUT: api/ShuttleTexts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShuttleText([FromRoute] int id, [FromBody] ShuttleText shuttleText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shuttleText.Id)
            {
                return BadRequest();
            }

            _context.Entry(shuttleText).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShuttleTextExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShuttleTexts
        [HttpPost]
        public async Task<IActionResult> PostShuttleText([FromBody] ShuttleText shuttleText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShuttleTexts.Add(shuttleText);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShuttleText", new { id = shuttleText.Id }, shuttleText);
        }

        // DELETE: api/ShuttleTexts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShuttleText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shuttleText = await _context.ShuttleTexts.FindAsync(id);
            if (shuttleText == null)
            {
                return NotFound();
            }

            _context.ShuttleTexts.Remove(shuttleText);
            await _context.SaveChangesAsync();

            return Ok(shuttleText);
        }

        private bool ShuttleTextExists(int id)
        {
            return _context.ShuttleTexts.Any(e => e.Id == id);
        }
    }
}