using HotelManagementSystem.Areas.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    [Authorize]
    public class TermsController : Controller
    {
        private readonly ICMSRepository _repository;

        public TermsController(ICMSRepository repository)
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
