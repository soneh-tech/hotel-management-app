namespace HotelManagementSystemAPI.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string? CustomerReview { get; set;}
        public int? CustomerID { get; set; }
    }
}
