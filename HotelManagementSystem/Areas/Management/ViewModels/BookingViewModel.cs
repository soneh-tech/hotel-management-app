using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class BookingViewModel
    {
        public Booking booking { get; set; }
        public List<SelectListItem> customers { get; set; }
        public List<SelectListItem> roomTypes { get; set; }

    }
}
