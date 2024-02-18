using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomTypeController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoomType(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRoomTypeByID), new { id = roomType.RoomTypeID }, roomType);
        }

        [HttpGet]

        public async Task<IEnumerable<RoomType>> GetRoomType()
          => await _context.RoomTypes.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoomType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomTypeByID(int id)
        {
            var roomtype = await _context.RoomTypes.FindAsync(id);
            return roomtype == null ? NotFound() : Ok(roomtype);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRoomType(int id, RoomType roomType)
        {
            if (id != roomType.RoomTypeID) return BadRequest();
            _context.Entry(roomType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var RoomTypeToDelete = await _context.RoomTypes.FindAsync(id);
            if (RoomTypeToDelete == null) return NotFound();
            _context.RoomTypes.Remove(RoomTypeToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
