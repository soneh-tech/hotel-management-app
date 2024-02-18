namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "the email field is required")]
        [EmailAddress(ErrorMessage ="invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "the password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
