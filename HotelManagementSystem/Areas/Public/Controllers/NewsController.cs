using HotelManagementSystem.Areas.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    public class NewsController : Controller
    {
        private readonly ICMSRepository _repository;

        public NewsController(ICMSRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(EventsViewModel model)
        {
            model.contact = await _repository.GetContact();
            model.socialMedias = await _repository.GetSocialMedias();
            model.events = await _repository.GetEvents();
            return View(model);
        }
     
    }
}
