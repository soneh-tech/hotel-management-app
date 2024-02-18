using HotelManagementSystem.Areas.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;

namespace HotelManagementSystem.Areas.Public.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ICMSRepository _repository;
        private readonly IManagementRepository _man_repository;

        public RoomsController(ICMSRepository repository, IManagementRepository man_repository)
        {
            _repository = repository;
            _man_repository = man_repository;
        }
        public async Task<IActionResult> Index(RoomsViewModel model)
        {
            model.contact = await _repository.GetContact();
            model.rooms = await _man_repository.GetRoomTypes();
            model.socialMedias = await _repository.GetSocialMedias();

            return View(model);
        }
        public async  Task<IActionResult> Details(RoomDetailViewModel model, int id)
        {
            model.contact = await _repository.GetContact();
            model.room = await _man_repository.GetRoomType(id);
            model.socialMedias = await _repository.GetSocialMedias();

            return View(model);
        }
        public IActionResult Payment()
        {

            var test_key = "sk_test_3fdece23ba23892088a826c314d534bb1b928b73";/* ConfigurationManager.AppSettings["PayStackSecret"];*/
            var api = new PayStackApi(test_key);

            // Initializing a transaction
            var request = new TransactionInitializeRequest()
            {
                AmountInKobo = 10000,
                Email = "soneh2011@gmail.com",
                Reference=Guid.NewGuid().ToString(),
                CallbackUrl= "https://localhost:7159/Public/verify"
            };
            var response = api.Transactions.Initialize(request);
            if (response.Status)
            { return Redirect(response.Data.AuthorizationUrl); }
            // show response.Message
            else { ViewData["message"] = response.Message; }

            return View("payment_error");
        }

        public IActionResult Verify(string reference)
        {
            var test_key = "sk_test_3fdece23ba23892088a826c314d534bb1b928b73";/* ConfigurationManager.AppSettings["PayStackSecret"];*/
            var api = new PayStackApi(test_key);
            var response = api.Transactions.Verify(reference);
            if(response.Data.Status == "success")
            {
                return RedirectToAction("index");
            }
            else
            {
                return View("payment_error");
            }
        }
    }
}
