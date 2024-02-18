namespace HotelManagementSystem.Areas.Public.Models
{
    public class About
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="enter a description here")]
        public string Description { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDescription { get; set; }
        public string VideoURL { get; set; }
    }
}
