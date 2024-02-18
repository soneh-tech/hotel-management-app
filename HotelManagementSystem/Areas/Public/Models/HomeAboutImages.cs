namespace HotelManagementSystem.Areas.Public.Models
{
    public class HomeAboutImages
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "chosen image is required")]
        public string ImageURL { get; set; }
    }
}
