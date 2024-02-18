using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    public class SalesController : Controller
    {
        private readonly IManagementRepository _repository;
        public SalesController(IManagementRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(SalesViewModel model)
        {
            model.customers = await GetCustomers();
            ViewBag.products = await _repository.GetProductsInCategory(1);
            return View(model);
        }
        async Task<List<SelectListItem>> GetCustomers()
        {
            var customerList = new List<SelectListItem>();

            var customers = await _repository.GetCustomers();
            if (customers != null)
                foreach (var customer in customers)
                {
                    customerList.Add(new SelectListItem { Text = customer.Name, Value = Convert.ToString(customer.CustomerID) });
                }

            return customerList;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems(int id)
        {
           var result = await _repository.GetProductsInCategory(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var result = await _repository.GetProduct(id);
            return Json(result);
        }

    }
}
