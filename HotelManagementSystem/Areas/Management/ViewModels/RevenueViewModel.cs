namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class RevenueViewModel
    {
        public IEnumerable<Booking>? bookings { get; set; }
        public IEnumerable<Sale>? sales { get; set; }
    }
}
