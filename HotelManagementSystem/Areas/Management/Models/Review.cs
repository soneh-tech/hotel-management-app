namespace HotelManagementSystem.Areas.Management.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        [Required(ErrorMessage = "the customer review is required")]
        public string CustomerReview { get; set;}
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "how do you feel about this review")]
        public bool IsPositive { get; set; }
    }
}
