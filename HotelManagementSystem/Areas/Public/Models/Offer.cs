namespace HotelManagementSystem.Areas.Public.Models
{
    public class Offer
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "the offer field is required")]
        public string Title { get; set; }
    }
}
