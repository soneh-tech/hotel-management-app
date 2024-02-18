namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
        public  IEnumerable<Booking> Bookings { get; set; }
    }
}
