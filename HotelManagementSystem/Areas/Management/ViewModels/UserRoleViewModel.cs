namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            users = new List<UsersViewModel>();
        }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public List<UsersViewModel>? users { get; set; }
    }
}
