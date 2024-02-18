using Microsoft.AspNetCore.Authorization;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICMSRepository _repository;
        private readonly IManagementRepository _man_repository;

        public HomeController(ICMSRepository repository, IManagementRepository man_repository) 
        {
            _repository = repository;
            _man_repository= man_repository;
        }
        public async Task<IActionResult> Index(HomeViewModel model)
        {
            model.home = await _repository.GetHome();
            model.home_sliders = await _repository.GetSliders();
            model.contact = await _repository.GetContact();
            model.socialMedias = await _repository.GetSocialMedias();
            model.services = await _repository.GetHomeServices();
            model.home_aboutImages = await _repository.GetHomeImages();
            model.rooms = await _man_repository.GetRoomTypes();

            return View(model);
        }
     
    }
}
