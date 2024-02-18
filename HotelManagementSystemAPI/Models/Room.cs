namespace HotelManagementSystemAPI.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string? RoomNumber { get; set; }
        public int RoomTypeID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOccupied { get; set; }

    }
}
