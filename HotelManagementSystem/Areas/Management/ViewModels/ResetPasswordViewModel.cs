namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "the email field is required")]
        [EmailAddress(ErrorMessage = "invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "the password field is required")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "password does not match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "the confirm password field is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
