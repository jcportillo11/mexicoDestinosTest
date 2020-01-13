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
    public class ShuttlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShuttlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Shuttles
        [HttpGet]
        public IEnumerable<Shuttle> GetShuttles()
        {
            return _context.Shuttles;
        }

        // POST: api/Shuttles/Results
        [HttpPost]
        [Route("results")]
        public async Task<IActionResult> GetShuttlesResults([FromBody] ShuttleRequest Hotel) {
            var response = new List<ShuttleResults>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shuttles = await _context.Shuttles.
                                Include(s => s.Zone).ThenInclude(z => z.Airport).
                                Include(s => s.ShuttleText).
                                Where(s => s.ZoneCode == Hotel.ZoneCode && s.ShuttleText.Language == Hotel.Language).
                                Select(row => new ShuttleResults{
                                    Id = row.Id,
                                    AirportCode = row.Zone.Airport.IATA,
                                    AirportName = row.Zone.Airport.AirportName,
                                    ZoneCode = row.Zone.ZoneCode,
                                    ZoneName = row.Zone.ZoneName,
                                    ShuttleCode = row.ShuttleCode,
                                    ShuttleName = row.ShuttleText.ShuttleName,
                                    ShuttleContent = row.ShuttleText.ShuttleContent,
                                    GrossRate = (row.NetRate * (1 + row.MarkUp)).ToString("N"),
                                    TotalRate = (row.NetRate * (1 + row.MarkUp) * (1 + row.Tax)).ToString("N")
                                }).ToListAsync();


            if (shuttles == null || shuttles.Count() < 1)
            {
                return NotFound();
            };

            return Ok(shuttles);
        }

        // GET: api/Shuttles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShuttle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shuttle = await _context.Shuttles.FindAsync(id);

            if (shuttle == null)
            {
                return NotFound();
            }

            return Ok(shuttle);
        }

        // PUT: api/Shuttles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShuttle([FromRoute] int id, [FromBody] Shuttle shuttle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shuttle.Id)
            {
                return BadRequest();
            }

            _context.Entry(shuttle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShuttleExists(id))
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

        // POST: api/Shuttles
        [HttpPost]
        public async Task<IActionResult> PostShuttle([FromBody] Shuttle shuttle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shuttles.Add(shuttle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShuttle", new { id = shuttle.Id }, shuttle);
        }

        // DELETE: api/Shuttles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShuttle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shuttle = await _context.Shuttles.FindAsync(id);
            if (shuttle == null)
            {
                return NotFound();
            }

            _context.Shuttles.Remove(shuttle);
            await _context.SaveChangesAsync();

            return Ok(shuttle);
        }

        private bool ShuttleExists(int id)
        {
            return _context.Shuttles.Any(e => e.Id == id);
        }
    }
}