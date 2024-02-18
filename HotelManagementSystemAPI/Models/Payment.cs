namespace HotelManagementSystemAPI.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string? PaymentMode { get; set; }
        public double? PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int BookingID { get; set; }
    }
}
