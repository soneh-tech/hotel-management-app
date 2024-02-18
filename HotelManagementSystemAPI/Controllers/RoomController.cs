using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRoomByID), new { id = room.RoomID }, room);
        }

        [HttpGet]
        public async Task<IEnumerable<Room>> GetRoom()
          => await _context.Rooms.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomByID(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room == null ? NotFound() : Ok(room);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRoom(int id, Room room)
        {
            if (id != room.RoomID) return BadRequest();
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var RoomToDelete = await _context.Rooms.FindAsync(id);
            if (RoomToDelete == null) return NotFound();
            _context.Rooms.Remove(RoomToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
