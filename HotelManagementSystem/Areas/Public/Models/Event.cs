namespace HotelManagementSystem.Areas.Public.Models
{
    public class Event
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="the title of event is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "the event date field is required")]
        public DateTime NewsDate { get; set; }
        [Required(ErrorMessage = "the event image field is required")]
        public string ImageURL { get; set; }


    }
}
