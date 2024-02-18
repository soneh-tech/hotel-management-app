using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    public class ErrorController : Controller
    {
        private int error_code;
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    error_code = 400;
                    break;
                case 403:
                    error_code = 403;
                    break;
                case 404:
                    error_code = 404;
                    break;
                case 500:
                    error_code = 500;
                    break;
                case 503:
                    error_code = 503;
                    break;
            }

            ViewBag.StatusCode = error_code;
            return View("/Areas/Management/Views/Shared/Error.cshtml");
        }
    }
}
