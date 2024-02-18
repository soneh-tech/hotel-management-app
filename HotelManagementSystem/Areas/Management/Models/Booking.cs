
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookDate { get; set; }
        [Required(ErrorMessage ="please select a check in date")]
        public DateTime CheckIn  { get; set; }
        [Required(ErrorMessage = "please select a check out date")]
        public DateTime CheckOut { get; set; }
        public string Reference { get; set; }
        public int Adult { get; set; }
        [Required(ErrorMessage = "please select a room for the customer")]
        public int RoomID { get; set; }
        [Required(ErrorMessage = "customer you are reserving room for is required")]
        public int CustomerID { get; set; }
        public string Status { get; set; }
        public bool IsCheckedOut { get; set; }
        [Required(ErrorMessage = "please select a room type for the customer")]
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
        public Payment Payment { get; set; }
        public Customer Guest { get; set; }
    }
}
