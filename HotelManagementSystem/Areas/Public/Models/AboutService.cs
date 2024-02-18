namespace HotelManagementSystem.Areas.Public.Models
{
    public class AboutService
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "the services field is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "the services description field is required")]
        public string ImageURL { get; set; }

    }
}
