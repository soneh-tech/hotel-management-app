
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting.Internal;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles ="Admin,Waiter")]
    public class RestaurantController : Controller
    {
        private readonly IManagementRepository _repository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public RestaurantController(IManagementRepository repository, IWebHostEnvironment hostingEnvironment)
        {
            _repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index(RestaurantViewModel model)
        {
            model.foods = await _repository.GetProductsInCategory(1);
            model.drinks = await _repository.GetProductsInCategory(2);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _repository.GetProduct(id);
            return PartialView(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            string image_url = "";
            if (model.Photo != null)
            {
                image_url = await ProcessPhoto(model);
            }
            var product = new Product
            {
                ImageURL = image_url,
                ProductName = model.Product.ProductName,
                ProductCategoryID = model.Product.ProductCategoryID,
                UnitPrice = model.Product.UnitPrice
            };
            await _repository.CreateProduct(product);

            return RedirectToAction("index");
        }
        private async Task<string> ProcessPhoto(ProductViewModel model)
        {
            string uniqueFile;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/products");
            uniqueFile = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFile);
            using (var filestream = new FileStream(filePath, FileMode.Create))
            { await model.Photo.CopyToAsync(filestream); }

            return uniqueFile;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ModifyProduct(ProductViewModel model)
        {
            string image_url = "";
            if (model.Photo != null)
            {
                if (!string.IsNullOrEmpty(model.Product.ImageURL))
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                        "images/products", model.Product.ImageURL);
                    System.IO.File.Delete(filepath);

                }

                image_url = await ProcessPhoto(model);

            }
            var product = new Product
            {
                ProductID = model.Product.ProductID,
                ImageURL = image_url,
                ProductName = model.Product.ProductName,
                ProductCategoryID = model.Product.ProductCategoryID,
                UnitPrice = model.Product.UnitPrice
            };
            await _repository.UpdateProduct(product);

            return RedirectToAction("index");

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int ProductID)
        {
            var product = await _repository.GetProduct(ProductID);
            if (!string.IsNullOrEmpty(product.ImageURL))
            {
                string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                       "images/products", product.ImageURL);
                System.IO.File.Delete(filepath);
            }
            await _repository.DeleteProduct(ProductID);
            return RedirectToAction("index");

        }

   
    }
}
