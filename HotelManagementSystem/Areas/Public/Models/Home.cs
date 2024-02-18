namespace HotelManagementSystem.Areas.Public.Models
{
    public class Home
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "the title field is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "the description field is required")]
        public string Description { get; set; }
        public string AboutTitle { get; set; }
        public string AboutDescription { get; set; }
    }
}
