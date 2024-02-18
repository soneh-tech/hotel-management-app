using HotelManagementSystem.Areas.Management.Models;

namespace HotelManagementSystem.Repositories.Management
{
    public interface IManagementRepository
    {
        Task<IEnumerable<Booking>> GetDashboardRecentBookings();
        Task<IEnumerable<Booking>> GetRecentBookings();
        Task<IEnumerable<Booking>> GetTodayBookings();
        Task<IEnumerable<Review>> GetReviews();
        Task<double> GeBookingAmount();
        Task<Booking> GetBooking(int id);
        Task<Booking> AssignGetBooking(int id);
        Task CreateBooking(Booking booking);
        Task ValidateBooking(Payment payment);
        Task UpdateBooking(int id);
        Task CheckOutBooking(int id);
        Task DeleteBooking(int id);
        Task AutoCancelBooking();
        Task AutoCheckOutBooking();
        Task ExtendBooking(Booking booking);
        Task AssignRoom(Booking booking);

        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Booking>> GetCustomerBookings(int id);
        Task<IEnumerable<Booking>> GetBookingHistory();
        Task<IEnumerable<Booking>> GetBookingHistory(DateTime from, DateTime to);
        Task<IEnumerable<Sale>> GetRestaurantHistory(DateTime from, DateTime to);
        Task<IEnumerable<Booking>> GetTodayBookingHistory();
        Task<IEnumerable<Sale>> GetTodayRestaurantHistory();
        Task CreateCustomer(Customer customer);
        Task<Customer> GetCustomer(int id);
        Task UpdateCustomer(Customer customer);

        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<RoomType> GetRoomType(int id);
        Task CreateRoomType(RoomType roomType);
        Task UpdateRoomType(RoomType roomType);
        Task DeleteRoomType(int id);

        Task CreateRoomImages(int id, List<string> images);
        Task<IEnumerable<RoomImage>> GetRoomImage(int id);
        Task DeleteRoomImages(IEnumerable<RoomImage> images);

        Task<IEnumerable<Room>> GetRoomsForType(int id);
        Task<IEnumerable<Room>> GetAvaiableRooms();

        Task<IEnumerable<Booking>> CheckAvailability();

        Task<IEnumerable<Room>> GetRooms();
        Task<Room> GetRoom(int id);
        Task CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);

        Task AddServicesToRoomType(int id, ICollection<Ammenity> ammenities);
        Task<IEnumerable<Ammenity>> GetAmmenities();
        Task<Ammenity> GetAmmenity(int id);
        Task CreateAmmenity(Ammenity ammenity);
        Task UpdateAmmenity(Ammenity ammenity);
        Task DeleteAmmenity(int id);


        Task<IEnumerable<Product>> GetProductsInCategory(int category_id);
        Task<Product> GetProduct(int id);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);


        Task<IEnumerable<Sale>> GetSales();
        Task<Sale> GetSale(int id);
        Task CreateSale(Sale sale);
        Task UpdateSale(Sale sale);
        Task DeleteSale(int id);
        Task<IEnumerable<object>> GetRevenueReport();
        Task<IEnumerable<object>> GetGeneralDailyReport();
        Task<IEnumerable<object>> GetGeneralDailyReport(DateTime from, DateTime to);
        Task<IEnumerable<object>> GetGeneralWeeklyReport(DateTime from,DateTime to);
        Task<IEnumerable<object>> GetGeneralMonthlyReport(DateTime from, DateTime to);
        Task<IEnumerable<object>> GetGeneralYearlyReport(DateTime from, DateTime to);

    }
}
