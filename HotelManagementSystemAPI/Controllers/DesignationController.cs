using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DesignationController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDesignation(Designation designation)
        {
            await _context.Designations.AddAsync(designation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDesignationByID), new { id = designation.DesignationID }, designation);
        }

        [HttpGet]
        public async Task<IEnumerable<Designation>> GetDesignation()
          => await _context.Designations.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Designation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDesignationByID(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            return designation == null ? NotFound() : Ok(designation);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDesignation(int id, Designation designation)
        {
            if (id != designation.DesignationID) return BadRequest();
            _context.Entry(designation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var DesignationToDelete = await _context.Designations.FindAsync(id);
            if (DesignationToDelete == null) return NotFound();
            _context.Designations.Remove(DesignationToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
