

using HotelManagementSystem.Repositories.Management;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles ="Admin,Receptionist")]
    public class CustomersController : Controller
    {
        private readonly IManagementRepository _repository;

        public CustomersController(IManagementRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {

            var result = await _repository.GetCustomers();
            return View(result);

        }
        public async Task<IActionResult> GuestDetails(int id)
        {
            
            var customer = await _repository.GetCustomer(id);
            var bookings = await _repository.GetCustomerBookings(id);
            var model = new CustomerViewModel {Customer = customer, Bookings = bookings };
            return View(model);

        }

        public async Task<IActionResult> GuestPopupDetails(int id)
        {
            var result = await _repository.GetCustomer(id);
            return PartialView("_ModifyGuestPopup", result);

        }

        public async Task<IActionResult> ModifyGuestDetails(Customer customer)
        {
            if (ModelState.IsValid)
                await _repository.UpdateCustomer(customer);
            return RedirectToAction("index");


        }

       
    }
}
