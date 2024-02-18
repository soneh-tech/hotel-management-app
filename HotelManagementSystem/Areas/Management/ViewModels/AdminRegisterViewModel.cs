using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Required(ErrorMessage = "the firstname field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "the lastname field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "the email field is required")]
        [EmailAddress(ErrorMessage = "invalid email format")]
        [Remote("IsEmailInUse", "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "the password field is required")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "password does not match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "the confirm password field is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
