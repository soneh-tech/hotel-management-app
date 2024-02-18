using Microsoft.AspNetCore.Authorization;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ReportController : Controller
    {
        private readonly IManagementRepository _repository;
        public ReportController(IManagementRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Occupancy()
        {
            var bookings = await _repository.GetTodayBookingHistory();
            var model = new OccupancyViewModel { Bookings = bookings };
            return View(model);
        }
        public async Task<JsonResult> GetOccupancyReport(DateTime from, DateTime to)
        {
            var result = await _repository.GetBookingHistory(from, to);
            return Json(result);
        }
        public IActionResult Revenue()
        {
            return View();
        }
        public async Task<JsonResult> GetTodayRevenueReport()
        {
            var result = await _repository.GetGeneralDailyReport();

            return Json(result);
        }
        public async Task<JsonResult> GetDailyRevenueReport(DateTime from, DateTime to)
        {
            var result = await _repository.GetGeneralDailyReport(from, to);

            return Json(result);
        }

        public async Task<JsonResult> GetWeeklyRevenueReport(DateTime from,DateTime to)
        {
            var result = await _repository.GetGeneralWeeklyReport(from,to);

            return Json(result);
        }
        public async Task<JsonResult> GetMonthlyRevenueReport(DateTime from, DateTime to)
        {
            var result = await _repository.GetGeneralMonthlyReport(from, to);

            return Json(result);
        }
        public async Task<JsonResult> GetYearlyRevenueReport(DateTime from, DateTime to)
        {
            var result = await _repository.GetGeneralYearlyReport(from, to);

            return Json(result);
        }
        public async Task<IActionResult> Restaurant()
        {
            var sales = await _repository.GetTodayRestaurantHistory();
            var model = new RestaurantReportViewModel { Sales = sales };
            return View(model);
        }
        public async Task<JsonResult> GetRestaurantReport(DateTime from, DateTime to)
        {
            var result = await _repository.GetRestaurantHistory(from, to);
            return Json(result);
        }
    }
}
