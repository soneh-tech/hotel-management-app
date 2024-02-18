using HotelManagementSystemAPI.Models;

namespace HotelManagementSystemAPI.ViewModels
{
    public class BookingViewModel
    {
        public Booking booking { get; set; }    
        public Customer customer { get; set; }
    }
}
