using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReviewByID), new { id = review.ReviewID }, review);
        }

        [HttpGet]
        public async Task<IEnumerable<Review>> GetReview()
          => await _context.Reviews.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Review), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewByID(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            return review == null ? NotFound() : Ok(review);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.ReviewID) return BadRequest();
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var ReviewToDelete = await _context.Reviews.FindAsync(id);
            if (ReviewToDelete == null) return NotFound();
            _context.Reviews.Remove(ReviewToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
