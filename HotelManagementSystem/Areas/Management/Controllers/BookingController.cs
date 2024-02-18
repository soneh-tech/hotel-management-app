
using HotelManagementSystem.Areas.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin,Manager,Receptionist")]
    public class BookingController : Controller
    {

        private readonly IManagementRepository _repository;
        public BookingController(IManagementRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            await _repository.AutoCancelBooking();
            await _repository.AutoCheckOutBooking();
            var result = await _repository.GetRecentBookings();
            return View(result);
        }
        public async Task<IActionResult> GetBooking(int id)
        {
            var result = await _repository.GetBooking(id);
            return PartialView(result);
        }
        public async Task<IActionResult> GetBookingForAssign(int id)
        {
            var item = await _repository.AssignGetBooking(id);
            var model = new AssignViewModel { booking = item, rooms = await GetRoomsFor(item.RoomTypeID)};
            if (model.rooms.Count is 0)
            {
                var result = await _repository.GetRecentBookings();
                return PartialView("_IndexPartialView", result);
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> ValidateBooking(Payment payment)
        {
            if (ModelState.IsValid)
            {
                await _repository.ValidateBooking(payment);
                var result = await _repository.GetRecentBookings();
                return PartialView("_IndexPartialView",result);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public async Task<IActionResult> AssignRoom(AssignViewModel model)
        {
           
                await _repository.AssignRoom(model.booking);

            return RedirectToAction("index");


        }
        public IActionResult Availability()
        {
            var result = _repository.CheckAvailability();
            return View();
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
        async Task<List<SelectListItem>> GetRoomTypes()
        {
            var roomTypeList = new List<SelectListItem>();

            var roomTypes = await _repository.GetRoomTypes();
            if (roomTypes != null)
                foreach (var roomType in roomTypes)
                {
                    roomTypeList.Add(new SelectListItem { Text = roomType.Name, Value = Convert.ToString(roomType.RoomTypeID) });
                }
            return roomTypeList;
        }
        async Task<List<SelectListItem>> GetRoomsFor(int room_id)
        {
            var roomList = new List<SelectListItem>();

            var rooms = await _repository.GetRoomsForType(room_id);
            if (rooms != null)
                foreach (var room in rooms)
                {
                    roomList.Add(new SelectListItem { Text = $"Romm {room.RoomNumber}", Value = Convert.ToString(room.RoomID) });
                }

            return roomList;
        }
        int GetReference()
        {
            var random_num = new Random(100000);
            return random_num.Next();
        }
        public async Task<IActionResult> Reservation(BookingViewModel model)
        {
            model.customers = await GetCustomers();
            model.roomTypes = await GetRoomTypes();
            return View(model);
        }
        public async Task<IActionResult> ReserveRoom(BookingViewModel model)
        {
         
            var booking = new Booking
            {
                BookDate = DateTime.UtcNow,
                CheckIn = model.booking.CheckIn,
                CheckOut = model.booking.CheckOut.AddHours(12),
                Reference = $"BKN-{GetReference()}",
                Adult = model.booking.Adult,
                RoomID = 0,
                RoomTypeID = model.booking.RoomTypeID,
                CustomerID = model.booking.CustomerID,
                Status = "pending"
            };
            await _repository.CreateBooking(booking);

           return RedirectToAction("index");
        }
        public async Task<IActionResult> NewGuest(BookingViewModel model)
        {
            var customer = new Customer
            {
                Name = model.booking.Guest.Name,
                Phone = model.booking.Guest.Phone,
                Email = model.booking.Guest.Email,
                Address = model.booking.Guest.Address
            };
            await _repository.CreateCustomer(customer);

            return RedirectToAction("reservation");

        }
        public async Task<IActionResult> CheckOutBooking(int id)
        {
            await _repository.CheckOutBooking(id);
            var result = await _repository.GetRecentBookings();
            return PartialView("_IndexPartialView", result);
        }
        public async Task<IActionResult> CancelBooking(Booking booking)
        {
            await _repository.DeleteBooking(booking.BookingID);
            var result = await _repository.GetRecentBookings();
            return PartialView("_IndexPartialView", result);
        }
        public async Task<IActionResult> ExtendBooking(Booking booking)
        {
            booking.CheckOut.AddHours(12);
            await _repository.ExtendBooking(booking);
            var result = await _repository.GetRecentBookings();
            return PartialView("_IndexPartialView", result);
        }
    }

}
