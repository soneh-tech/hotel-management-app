namespace HotelManagementSystem.Areas.Management.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        [Required(ErrorMessage = "the room number is required")]
        public string RoomNumber { get; set; }
        public int RoomTypeID { get; set; }
        [Required(ErrorMessage = "please specify the room condition")]
        public bool IsActive { get; set; }
        public bool IsOccupied { get; set; }

    }
}
