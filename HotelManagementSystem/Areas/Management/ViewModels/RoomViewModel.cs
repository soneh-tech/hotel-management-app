using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class RoomViewModel
    {
        public IEnumerable<Room> rooms { get; set; }
        public List<SelectListItem> roomTypes { get; set; }
    }
}
