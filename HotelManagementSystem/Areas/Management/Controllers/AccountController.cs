
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mail;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public AccountController(SignInManager<AppUser> signInManager,
                                UserManager<AppUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AdminLoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed &&
                    (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError("", "Sorry, your email has not been confirmed yet");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin") || await userManager.IsInRoleAsync(user, "Manager"))
                        {

                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (await userManager.IsInRoleAsync(user, "Receptionist"))
                        {

                            return RedirectToAction("Index", "Booking");
                        }
                        else if (await userManager.IsInRoleAsync(user, "Waiter"))
                        {

                            // return RedirectToAction("Index", "Booking");
                        }
                    }
                }
                ModelState.AddModelError("", "invalid login attempt");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AdminRegisterViewModel model, string? id)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            var model_result = await GetUsers(id);
                            SendEmail("Email Confirmation",
                          $"please click the link or copy and paste in your browser to confirm your email {confirmationLink}",
                          model.Email);
                            return PartialView("/Areas/management/Views/Administration/_ModifyUsersPartialView.cshtml", model_result);
                        }
                    }

                    else
                    {
                        SendEmail("Email Confirmation",
                     $"please click the link or copy and paste in your browser to confirm your email {confirmationLink}",
                     model.Email);
                        ViewBag.Message = "A confirmation mail has been sent to your provided email";
                        return View("Confirmation");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId != null && token != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await userManager.ConfirmEmailAsync(user, token);
                    if (result.Succeeded)
                    {
                        ViewBag.Message = "Congratulations!!! your email was confirmed";
                        return View();
                    }
                }
            }
            ViewBag.Message = "Sorry, we are unable to confirm this email address at the moment";
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.Message = "Password reset successful";
                        return View("Confirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                ViewBag.Message = "Password reset successful";
                return View("Confirmation");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (email != null && token != null)
            {
                return View();
            }
            ModelState.AddModelError("", "invalid password reset token");
            return View();
        }
        public static void SendEmail(string emailSubject, string emailBody, string toEmail)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("info@wallgateapartments.com.ng", "Wallgate Apartments");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailBody;
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.wallgateapartments.com.ng";
                    smtpClient.Port = 8889;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    var credentials = new NetworkCredential()
                    {
                        UserName = "info@wallgateapartments.com.ng",
                        Password = "Wallgate123_"
                    };
                    smtpClient.Credentials = credentials;
                    smtpClient.Send(mailMessage);

                };
            };


        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(User.Identity.Name);
                    if (user != null && await userManager.IsEmailConfirmedAsync(user))
                    {
                        var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                        if (result.Succeeded)
                        {
                            await signInManager.RefreshSignInAsync(user);
                            return RedirectToAction("profile");
                        }
                        foreach (var error in result.Errors)
                        {
                            ViewBag.Errors.Add(error.Description);
                        }
                        return RedirectToAction("profile");
                    }
                    return RedirectToAction("index");
                }
                return RedirectToAction("profile");
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var confirmationLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);

                    SendEmail("Password Recovery",
                        $"please click the link or copy and paste in your browser to reset your password {confirmationLink}",
                        model.Email);

                    ViewBag.Message = "If you have an account with us, a password reset link would be sent to your email";
                    return View("Confirmation");
                }
                ViewBag.Message = "If you have an account with us, a password reset link would be sent to your email";
                return View("Confirmation");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Authorize]
        public async Task<List<UsersViewModel>> GetUsers(string id)
        {
            var users = new List<UsersViewModel>();
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
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
            return users;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            if (result is null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use by someone");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var model = new ProfileViewModel
                    {
                        ID = user.Id,
                        Address = user.Address,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("logout");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.ID);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index");
        }

    }
}
