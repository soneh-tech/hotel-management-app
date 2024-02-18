using HotelManagementSystem.Areas.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICMSRepository _repository;

        public ContactController(ICMSRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            model.contact = await _repository.GetContact();
            model.socialMedias = await _repository.GetSocialMedias();

            return View(model);
        }
    
    }
}
