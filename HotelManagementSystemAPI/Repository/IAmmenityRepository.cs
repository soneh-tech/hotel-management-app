using HotelManagementSystemAPI.Models;

namespace HotelManagementSystemAPI.Repository
{
    public interface IAmmenityRepository
    {
         Task<Ammenity> GetAmmenityAsync(int ID);
         Task<Ammenity> CreateAmmenityAsync(Ammenity ammenity);
         Task<IEnumerable<Ammenity>> GetAmmenityAsync();
         Task<Ammenity> UpdateAmmenityAsync(Ammenity ammenity);
         void DeleteAmmenityAsync(int ID);
    }
}
