using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemCategoryController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateItemCategory(ItemCategory category)
        {
            await _context.ItemCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemCategoryByID), new { id = category.ItemCategoryID }, category);
        }

        [HttpGet]
        public async Task<IEnumerable<ItemCategory>> GetItemCategory()
          => await _context.ItemCategories.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItemCategoryByID(int id)
        {
            var category = await _context.ItemCategories.FindAsync(id);
            return category == null ? NotFound() : Ok(category);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateItemCategory(int id, ItemCategory category)
        {
            if (id != category.ItemCategoryID) return BadRequest();
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            var ItemCategoryToDelete = await _context.ItemCategories.FindAsync(id);
            if (ItemCategoryToDelete == null) return NotFound();
            _context.ItemCategories.Remove(ItemCategoryToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
