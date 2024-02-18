namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "the email field is required")]
        [EmailAddress(ErrorMessage = "invalid email format")]
        public string Email { get; set; }
    }
}
