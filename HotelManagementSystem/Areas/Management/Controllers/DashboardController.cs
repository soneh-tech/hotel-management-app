
using HotelManagementSystem.Repositories.Management;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DashboardController : Controller
    {
        private readonly IManagementRepository _repository;

        public DashboardController(IManagementRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(DashboardViewModel model)
        {
            var bookings = await _repository.GetTodayBookings();
            var rooms = await _repository.GetAvaiableRooms();
            var reviews = await _repository.GetReviews();
            var booking_amount = await _repository.GeBookingAmount();
            var positive_reviews = reviews.Where(r => r.IsPositive == true).ToList();
            var negative_reviews = reviews.Where(r => r.IsPositive == false).ToList();
            model.today_bookings = bookings.Count();
            model.available_rooms = rooms.Count();
            model.total_reviews = reviews.Count();
            model.positive_reviews = positive_reviews.Count();
            model.negative_reviews = negative_reviews.Count();
            model.booking_amount_generated = booking_amount;
            model.bookings = await _repository.GetDashboardRecentBookings();
            return View(model);
        }

        public async Task<JsonResult> GetRevenueReport()
        {
            var result = await _repository.GetRevenueReport();

            return Json(result);
        }
    }
}
