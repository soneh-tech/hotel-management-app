namespace HotelManagementSystem.Areas.Management.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "the payment mode is required")]
        public string PaymentMode { get; set; }
        [Required(ErrorMessage = "the amount paid is required")]
        public double PaidAmount { get; set; }
        [Required(ErrorMessage = "the payment date is required")]
        public DateTime PaymentDate { get; set; }
        public int BookingID { get; set; }
    }
}
