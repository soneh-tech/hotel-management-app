namespace HotelManagementSystemAPI.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string? Reference { get; set; }
        public int? Adult { get; set; }
        public int? RoomID { get; set; }
        public int? CustomerID { get; set; }
        public string? Status { get; set; }
        public Payment? Payment { get; set; }
        public Customer? Guest { get; set; }
    }
}
