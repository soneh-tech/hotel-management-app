using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class ProfileViewModel
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "the firstname field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "the lastname field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "the phone number field is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "the address field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "the email field is required")]
        [EmailAddress(ErrorMessage = "invalid email format")]
        public string Email { get; set; }

        
    }
}
