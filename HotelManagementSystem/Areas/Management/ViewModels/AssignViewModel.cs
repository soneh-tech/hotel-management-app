using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class AssignViewModel
    {
        public Booking booking { get; set; }
        public List<SelectListItem> rooms { get; set; }
    }
}
