namespace HotelManagementSystem.Areas.Public.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="the phone field is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "the email field is required")]
        [EmailAddress(ErrorMessage = "invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "the address field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "the description field is required")]
        public string ShortDescription { get; set; }

    }
}
