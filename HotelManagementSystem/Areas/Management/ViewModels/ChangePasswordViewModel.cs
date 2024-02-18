namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "the current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "the new password field is required")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "new password does not match")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "the confirm password field is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
