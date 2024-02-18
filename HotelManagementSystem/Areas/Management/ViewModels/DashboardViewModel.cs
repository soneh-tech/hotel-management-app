using HotelManagementSystem.Areas.Management.Models;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class DashboardViewModel
    {
        public int  today_bookings { get; set; }
        public int available_rooms { get; set; }
        public double booking_amount_generated { get; set; }
        public double food_amount_generated { get; set; }
        public int total_reviews { get; set; }
        public int positive_reviews { get; set; }
        public int negative_reviews { get; set; }
        public IEnumerable<Booking>? bookings { get; set; }
    }
}
