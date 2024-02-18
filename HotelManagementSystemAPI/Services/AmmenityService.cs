using HotelManagementSystemAPI.Data;
using HotelManagementSystemAPI.Models;
using HotelManagementSystemAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Services
{
    public class AmmenityService : IAmmenityRepository
    {
        private readonly AppDbContext _context;

        public AmmenityService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<Ammenity> CreateAmmenityAsync(Ammenity ammenity)
        {
           await _context.Ammenities.AddAsync(ammenity);
           await _context.SaveChangesAsync();
           return ammenity;
        }

        public async void DeleteAmmenityAsync(int ID)
        {
            var ammenity_to_delete = await _context.Ammenities.FindAsync(ID);
            if (ammenity_to_delete != null)
                _context.Remove(ammenity_to_delete);
            await _context.SaveChangesAsync();
           
       
        }

        public async Task<Ammenity> GetAmmenityAsync(int id)
        {
            var ammenity = await _context.Ammenities.FindAsync(id);
            return ammenity;
          
        }

        public async Task<IEnumerable<Ammenity>> GetAmmenityAsync()
            => await _context.Ammenities.ToListAsync();

        public async Task<Ammenity> UpdateAmmenityAsync(Ammenity ammenity)
        {
           _context.Entry(ammenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ammenity;

        }
    }
}
