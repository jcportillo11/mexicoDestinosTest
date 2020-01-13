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
    public class HotelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public IEnumerable<Hotel> GetHotels()
        {
            return _context.Hotels.
                        Include(h => h.Destination).ThenInclude(d => d.Country).
                        Include(h => h.Zone).ThenInclude(z => z.Airport).ToList();
        }

        // GET: api/hotels/autocomplete?term=oas
        [HttpGet]
        [Route("autocomplete")]
        public IEnumerable<HotelBase> GetHotelsAutocomplete(string term)
        {
            var response = new List<HotelBase>();
            if (string.IsNullOrEmpty(term))
            {
                var noTerm = new HotelBase()
                {
                    HotelName = "Escribe una palabra para buscar tu hotel",
                    ZoneCode = string.Empty
                };                
                response.Add(noTerm);
                return response;
            };
            response = _context.Hotels.
                        Include(h => h.Destination).ThenInclude(d => d.Country).
                        Include(h => h.Zone).ThenInclude(z => z.Airport).
                        Where(h => h.HotelName.Contains(term)).ToList().
                        Select(row => new HotelBase { HotelName = row.HotelName, ZoneCode = row.ZoneCode }).ToList();

            if (response.Count() < 1)
            {
                var noHotel = new HotelBase()
                {
                    HotelName = "No se encontró el hotel que buscabas",
                    ZoneCode = string.Empty
                };
                response.Add(noHotel);
                return response;
            };

            return response;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotel = await _context.Hotels.
                        Include(h => h.Destination).ThenInclude(d => d.Country).
                        Include(h => h.Zone).ThenInclude(z => z.Airport).FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel([FromRoute] int id, [FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        [HttpPost]
        public async Task<IActionResult> PostHotel([FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok(hotel);
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}