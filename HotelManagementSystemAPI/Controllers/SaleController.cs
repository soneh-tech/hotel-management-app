using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
            => this._context = context;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSale(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSaleByID), new { id = sale.SaleID }, sale);
        }

        [HttpGet]
        public async Task<IEnumerable<Sale>> GetSale()
          => await _context.Sales.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleByID(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            return sale == null ? NotFound() : Ok(sale);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSale(int id, Sale sale)
        {
            if (id != sale.SaleID) return BadRequest();
            _context.Entry(sale).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var SaleToDelete = await _context.Sales.FindAsync(id);
            if (SaleToDelete == null) return NotFound();
            _context.Sales.Remove(SaleToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
