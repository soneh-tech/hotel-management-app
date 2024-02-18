using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmmenityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AmmenityController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAmmenity(Ammenity ammenity)
        {
            await _context.Ammenities.AddAsync(ammenity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAmmenityByID), new { id = ammenity.AmmenityID }, ammenity);
        }

        [HttpGet]
        public async Task<IEnumerable<Ammenity>> GetAmmenities()
          => await _context.Ammenities.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ammenity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAmmenityByID(int id)
        {
            var ammenity = await _context.Ammenities.FindAsync(id);
            return ammenity == null ? NotFound() : Ok(ammenity);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAmmenity(int id, Ammenity ammenity)
        {
            if (id != ammenity.AmmenityID) return BadRequest();
            _context.Entry(ammenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAmmenity(int id)
        {
            var AmmenityToDelete = await _context.Ammenities.FindAsync(id);
            if (AmmenityToDelete == null) return NotFound();
            _context.Ammenities.Remove(AmmenityToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
