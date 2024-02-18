using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(List<UserRoleViewModel> model)
        {

            var roles = roleManager.Roles.ToList();
            var users = userManager.Users.ToList();
            foreach (var role in roles)
            {
                var single_model = new UserRoleViewModel();
                single_model.RoleID = role.Id;
                if (!string.IsNullOrEmpty(role.Name))
                    single_model.RoleName = role.Name;
                foreach (var user in users)
                {
                    var my_user = new UsersViewModel();
                    if (!string.IsNullOrEmpty(role.Name))
                        if (await userManager.IsInRoleAsync(user, role.Name))
                        {
                            if (!string.IsNullOrEmpty(user.UserName))
                            {

                                my_user.UserID = user.Id;
                                my_user.UserName = user.UserName;
                                my_user.FirstName = user.FirstName;
                                my_user.LastName = user.LastName;

                            }
                            single_model.users.Add(my_user);
                        }
                }
                model.Add(single_model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ModifyUsersInRole(string id)
        {
            var users = new List<UsersViewModel>();
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                ViewBag.RoleID = role.Id; ViewBag.RoleName = role.Name;

                foreach (var user in userManager.Users.ToList())
                {
                    var my_user = new UsersViewModel();

                        my_user.UserID = user.Id;
                        my_user.FirstName = user.FirstName;
                        my_user.LastName = user.LastName;

                    if (!string.IsNullOrEmpty(role.Name))
                        if (await userManager.IsInRoleAsync(user, role.Name))
                            my_user.IsSelected = true;
                        else
                            my_user.IsSelected = false;

                    users.Add(my_user);
                }
            }
            return View(users);
        }


        [HttpPost]
        public async Task<IActionResult> ModifyUsersInRole(string id, List<UsersViewModel> users)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    var user = await userManager.FindByIdAsync(users[i].UserID);
                    IdentityResult result = null;
                    if (users[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!users[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else { continue; }
                    if (result.Succeeded)
                    {
                        if (i < (users.Count - 1))
                            continue;
                        else
                            return RedirectToAction("index");

                    }
                }
            }
            return RedirectToAction("index");
        }

    }
}
