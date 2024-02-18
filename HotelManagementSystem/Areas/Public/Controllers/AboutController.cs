using HotelManagementSystem.Areas.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    public class AboutController : Controller
    {
        private readonly ICMSRepository _repository;

        public AboutController(ICMSRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(AboutViewModel model)
        {

            model.about = await _repository.GeAbout();
            model.contact = await _repository.GetContact();
            model.offers = await _repository.GetOffers();
            model.socialMedias = await _repository.GetSocialMedias();
            model.galleries = await _repository.GetGalleries();
            model.services = await _repository.GetAboutServices(); 

            return View(model);
        }

    }
}
