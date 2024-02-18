using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using HotelManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookingByID), new { id = booking.BookingID }, booking);
        }

        [HttpGet]
        public async Task<IEnumerable<Booking>> GetBookings()
          => await _context.Bookings.Where(d => d.BookDate.Date >= DateTime.Today.AddDays(-7))
            .Include(g => g.Guest).OrderByDescending(x => x.BookingID)
            .AsNoTracking().ToListAsync();

     

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingByID(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateBooking(int id, Booking booking)
        {
            if (id != booking.BookingID) return BadRequest();
            var result = await _context.Bookings.FindAsync(id);
            result.Status = "success";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var BookingToDelete = await _context.Bookings.FindAsync(id);
            if (BookingToDelete == null) return NotFound();
            _context.Bookings.Remove(BookingToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
