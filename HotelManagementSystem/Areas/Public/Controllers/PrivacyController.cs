using HotelManagementSystem.Areas.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    [Authorize]
    public class PrivacyController : Controller
    {
        private readonly ICMSRepository _repository;

        public PrivacyController(ICMSRepository repository)
        {
            _repository = repository;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            return View();
        }
    }
}
