using NuGet.Packaging;
using System.Data;

namespace HotelManagementSystem.Services.Management
{
    public class ManagementService : IManagementRepository
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public ManagementService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task CreateBooking(Booking booking)
        {
            await context.Bookings.AddAsync(booking);
            await context.SaveChangesAsync();
        }

        public async Task CreateCustomer(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBooking(int id)
        {
            var BookingToDelete = await context.Bookings.FindAsync(id);
            if (BookingToDelete != null)
            {
                context.Bookings.Remove(BookingToDelete);
                var room = await context.Rooms.FindAsync(BookingToDelete.RoomID);
                if (room != null)
                    room.IsOccupied = false;
            }
            await context.SaveChangesAsync();
        }

        public async Task<Booking> GetBooking(int id)
        {
            var booking = await context.Bookings.FindAsync(id);

            return booking;
        }
        public async Task<Booking> AssignGetBooking(int id)
        {
            var result = await context.Bookings.Where(x => x.BookingID == id)
             .Include(r => r.RoomType).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<Booking>> GetRecentBookings()

         => await context.Bookings.Where(d => d.BookDate >= DateTime.UtcNow.AddDays(-7))
            .Include(g => g.Guest).Include(r => r.RoomType).OrderByDescending(x => x.BookingID)
            .AsNoTracking().ToListAsync();
        public async Task<IEnumerable<Booking>> GetDashboardRecentBookings()

       => await context.Bookings.Where(d => d.BookDate >= DateTime.UtcNow.AddDays(-7))
          .Include(g => g.Guest).Include(r => r.RoomType).Include(p => p.Payment).OrderByDescending(x => x.BookingID)
          .AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Customer>> GetCustomers()
        => await context.Customers.ToListAsync();

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
           => await context.RoomTypes.Include(a => a.Ammenities).Include(r => r.Rooms).Include(i => i.Images).ToListAsync();

        public async Task<IEnumerable<Room>> GetRoomsForType(int id)
            => await context.Rooms.Where(r => r.RoomTypeID == id && r.IsOccupied == false).ToListAsync();

        public async Task UpdateBooking(int id)
        {
            var result = await context.Bookings.FindAsync(id);
            if (result != null)
                result.Status = "success";
            await context.SaveChangesAsync();
        }
        public async Task AssignRoom(Booking booking)
        {
            var room = await context.Rooms.FindAsync(booking.RoomID);
            var my_booking = await context.Bookings.FindAsync(booking.BookingID);
            if (my_booking != null && room != null)
            {
                my_booking.RoomID = booking.RoomID;
                room.IsOccupied = true;
            }
            await context.SaveChangesAsync();
        }

        public async Task ValidateBooking(Payment payment)
        {
            await context.Payments.AddAsync(payment);
            var booking = await context.Bookings.FindAsync(payment.BookingID);
            if (booking != null)
            {
                booking.Status = "success";
            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetTodayBookings()
             => await context.Bookings.Where(d => d.BookDate == DateTime.UtcNow)
                .AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Room>> GetAvaiableRooms()
             => await context.Rooms.Where(d => d.IsOccupied == false && d.IsActive == true)
                .AsNoTracking().ToListAsync();

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);

            return customer;
        }
        public async Task<IEnumerable<Booking>> GetBookingHistory()
       => await context.Bookings.Where(d => d.BookDate < DateTime.UtcNow && d.Status == "success")
            .Include(g => g.Guest).Include(r => r.RoomType).OrderByDescending(x => x.BookingID)
            .AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Booking>> GetBookingHistory(DateTime from, DateTime to)
      => await context.Bookings
        .Where(b => b.BookDate >= from && b.BookDate <= to && b.Status == "success")
        .Include(g => g.Guest)
        .Include(p=>p.Payment)
        .Include(r => r.RoomType)
        .OrderByDescending(x => x.BookingID)
        .AsNoTracking()
        .ToListAsync();

        public async Task<IEnumerable<Sale>> GetRestaurantHistory(DateTime from, DateTime to)
      => await context.Sales
        .Where(d => d.SaleDate >= from && d.SaleDate <= to)
        .Include(g => g.Customer)
        .Include(p => p.Product)
        .OrderByDescending(x => x.SaleID)
        .AsNoTracking()
        .ToListAsync();

        public async Task<IEnumerable<Booking>> GetTodayBookingHistory()
       => await context.Bookings
         .Where(b => b.BookDate == DateTime.UtcNow && b.Status == "success")
         .Include(g => g.Guest)
         .Include(p => p.Payment)
         .Include(r => r.RoomType)
         .OrderByDescending(x => x.BookingID)
         .AsNoTracking()
         .ToListAsync();

        public async Task<IEnumerable<Sale>> GetTodayRestaurantHistory()
      => await context.Sales
        .Where(d => d.SaleDate == DateTime.UtcNow)
        .Include(g => g.Customer)
        .Include(p => p.Product)
        .OrderByDescending(x => x.SaleID)
        .AsNoTracking()
        .ToListAsync();

        public async Task UpdateCustomer(Customer customer)
        {
            var result = await context.Customers.FindAsync(customer.CustomerID);
            if (result != null)
            {
                result.Address = customer.Address;
                result.Phone = customer.Phone;
                result.Email = customer.Email;
                result.Name = customer.Name;
            }
            await context.SaveChangesAsync();
        }

        public async Task CheckOutBooking(int id)
        {
            var result = await context.Bookings.FindAsync(id);
            if (result != null)
            {
                result.IsCheckedOut = true;
                var room = await context.Rooms.FindAsync(result.RoomID);
                if (room != null)
                    room.IsOccupied = false;
            }
            await context.SaveChangesAsync();
        }

        public async Task ExtendBooking(Booking booking)
        {
            var result = await context.Bookings.FindAsync(booking.BookingID);
            if (result != null)
                result.CheckOut = booking.CheckOut;
            await context.SaveChangesAsync();
        }

        public async Task AutoCancelBooking()
        {
            var bookings = context.Bookings.ToList();
            foreach (var booking in bookings)
            {
                if ((DateTime.UtcNow >= booking.BookDate.AddDays(+1)) && booking.Status is "pending")
                {
                    context.Bookings.Remove(booking);
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task AutoCheckOutBooking()
        {
            var bookings = context.Bookings.ToList();
            foreach (var booking in bookings)
            {
                if ((booking.CheckOut < DateTime.UtcNow) && booking.Status is "success")
                {

                    booking.IsCheckedOut = true;
                    var room = await context.Rooms.FindAsync(booking.RoomID);
                    if (room != null)
                        room.IsOccupied = false;
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetCustomerBookings(int id)
        => await context.Bookings.Where(c => c.CustomerID == id)
            .Include(r => r.RoomType).OrderByDescending(x => x.BookingID)
            .AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Review>> GetReviews()
        => await context.Reviews.ToListAsync();

        public Task<IEnumerable<Booking>> CheckAvailability()
        {
            return null;
        }

        public async Task<double> GeBookingAmount()
        {
            double amount = 0;

            var booking = await context.Bookings.Where(d => d.BookDate.Month == DateTime.UtcNow.Month && d.Status == "success")
             .Include(p => p.Payment).OrderByDescending(x => x.BookingID)
             .AsNoTracking().ToListAsync();

            foreach (var item in booking)
            {
                if (item.Payment != null)
                    amount += item.Payment.PaidAmount;
            }

            return amount;
        }

        public async Task<RoomType> GetRoomType(int id)
        {
            var roomType = await context.RoomTypes.Where(x => x.RoomTypeID == id).Include(i => i.Images).FirstOrDefaultAsync();

            return roomType;
        }
        public async Task CreateRoomType(RoomType roomType)
        {
            await context.RoomTypes.AddAsync(roomType);
            await context.SaveChangesAsync();
        }
        public async Task AddServicesToRoomType(int id, ICollection<Ammenity> ammenities)
        {
            var result = await context.RoomTypes.FindAsync(id);
            if (result != null)
            {
                result.Ammenities.AddRange(ammenities);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateRoomType(RoomType roomType)
        {
            var result = await context.RoomTypes.FindAsync(roomType.RoomTypeID);
            if (result != null)
            {
                result.RoomTypeID = roomType.RoomTypeID;
                result.Name = roomType.Name;
                result.Size = roomType.Size;
                result.Capacity = roomType.Capacity;
                result.Price = roomType.Price;
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteRoomType(int id)
        {
            var RoomTypeToDelete = await context.RoomTypes.FindAsync(id);
            if (RoomTypeToDelete != null)
            {
                //var rooms = await context.Rooms.Where(r=>r.RoomTypeID == RoomTypeToDelete.RoomTypeID).ToListAsync();
                //if(rooms != null)
                //{
                //    foreach (var room in rooms)
                //    {
                //        context.Rooms.Remove(room);
                //    }
                //}
                context.RoomTypes.Remove(RoomTypeToDelete);

            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Room>> GetRooms()
      => await context.Rooms.ToListAsync();

        public async Task<Room> GetRoom(int id)
        {
            var room = await context.Rooms.Where(r => r.RoomID == id).FirstOrDefaultAsync();
            return room;
        }

        public async Task CreateRoom(Room room)
        {
            await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            var result = await context.Rooms.FindAsync(room.RoomID);
            if (result != null)
            {
                result.RoomNumber = room.RoomNumber;
                result.IsActive = room.IsActive;
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var RoomToDelete = await context.Rooms.FindAsync(id);
            if (RoomToDelete != null)
            {
                context.Rooms.Remove(RoomToDelete);

            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ammenity>> GetAmmenities()
          => await context.Ammenities.ToListAsync();

        public async Task<Ammenity> GetAmmenity(int id)
        {
            var ammenity = await context.Ammenities.Where(a => a.AmmenityID == id).FirstOrDefaultAsync();
            return ammenity;
        }

        public async Task CreateAmmenity(Ammenity ammenity)
        {
            await context.Ammenities.AddAsync(ammenity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAmmenity(Ammenity ammenity)
        {
            var result = await context.Ammenities.FindAsync(ammenity.AmmenityID);
            if (result != null)
            {
                result.Name = ammenity.Name;
                result.IsActive = ammenity.IsActive;
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteAmmenity(int id)
        {
            var AmmenityToDelete = await context.Ammenities.FindAsync(id);
            if (AmmenityToDelete != null)
            {
                context.Ammenities.Remove(AmmenityToDelete);

            }
            await context.SaveChangesAsync();
        }

        public async Task CreateRoomImages(int id, List<string> images)
        {
            List<RoomImage> result = new List<RoomImage>();
            foreach (var item in images)
            {
                var room = new RoomImage { RoomTypeID = id, ImageURL = item };
                result.Add(room);
            }
            await context.RoomImages.AddRangeAsync(result);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<RoomImage>> GetRoomImage(int id)
        {
            var result = await context.RoomImages.Where(x => x.RoomTypeID == id).ToListAsync();
            return result;
        }

        public async Task DeleteRoomImages(IEnumerable<RoomImage> images)
        {
            context.RoomImages.RemoveRange(images);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsInCategory(int category_id)
            => await context.Products.Where(x => x.ProductCategoryID == category_id).ToListAsync();

        public async Task<Product> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            return product;
        }

        public async Task CreateProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var result = await context.Products.FindAsync(product.ProductID);
            if (result != null)
            {
                result.ProductName = product.ProductName;
                result.UnitPrice = product.UnitPrice;
                result.ImageURL = product.ImageURL;
                result.ProductCategoryID = product.ProductCategoryID;
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var ProductToDelete = await context.Products.FindAsync(id);
            if (ProductToDelete != null)
            {
                context.Products.Remove(ProductToDelete);

            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetSales()
            => await context.Sales.ToListAsync();


        public async Task<Sale> GetSale(int id)
        {
            var sale = await context.Sales.FindAsync(id);
            return sale;
        }

        public async Task CreateSale(Sale sale)
        {
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSale(Sale sale)
        {
            var result = await context.Sales.FindAsync(sale.SaleID);
            if (result != null)
            {
                result.ProductID = sale.ProductID;
                result.CustomerID = sale.CustomerID;
                result.EmployeeID = sale.EmployeeID;
                result.Quantity = sale.Quantity;
                result.CostPrice = sale.CostPrice;
                result.SaleDate = DateTime.Now;
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteSale(int id)
        {
            var SaleToDelete = await context.Sales.FindAsync(id);
            if (SaleToDelete != null)
            {
                context.Sales.Remove(SaleToDelete);

            }
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<object>> GetRevenueReport()
        {
                var query = @"SELECT 
                                MONTH(b.bookdate) AS month_number,
                                DATENAME(month, b.bookdate) AS month_name,
                                SUM(p.paidamount) AS bookingamount,
                                SUM(s.costprice * s.quantity) AS saleamount
                                FROM 
                                    bookings b
                                JOIN 
                                    payments p ON b.bookingid = p.bookingid
                                LEFT JOIN 
                                    sales s ON MONTH(s.saledate) = MONTH(b.bookdate) AND YEAR(s.saledate) = YEAR(b.bookdate)
                                WHERE 
                                    (YEAR(b.bookdate) = YEAR(GETDATE()) AND b.status = 'success') OR CONVERT(DATE, s.SaleDate) = CONVERT(DATE, GETDATE())
                                GROUP BY 
                                    MONTH(b.bookdate), DATENAME(month, b.bookdate)
                                ORDER BY 
                                    month_number";
                SqlConnection con = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
                con.Open();
                var cmd = new SqlCommand(query, con);
                SqlDataReader temp = cmd.ExecuteReader();
                var reports = new List<object>();
                while (temp.Read())
                {
                    double sale_amount = 0;
                    if (temp["saleamount"] == DBNull.Value)
                        sale_amount = 0;
                    else
                        sale_amount = (double)temp["saleamount"];

                    var report = new 
                    {
                        MonthNumber = (int)temp["month_number"],
                        MonthName = (string)temp["month_name"],
                        BookingAmount = (double)temp["bookingamount"],
                        SaleAmount = sale_amount,
                    };
                    reports.Add(report);
                }
                con.Close();
                return reports;
        }
        public async Task<IEnumerable<object>> GetGeneralDailyReport()
        {
            try
            {

                var query = @"SELECT 
                            b.bookdate AS todaytime,
	                        p.paidamount AS bookingamount,
                            CASE WHEN c.categoryname = 'Food' THEN s.costprice * s.quantity ELSE 0 END AS foodamount,
                            CASE WHEN c.categoryname = 'Drink' THEN s.costprice * s.quantity ELSE 0 END AS drinkamount
                            FROM 
                                bookings b
                            JOIN 
                                payments p ON b.bookingid = p.bookingid
                            LEFT JOIN 
                                sales s ON s.saledate = b.bookdate
                            LEFT JOIN 
                                products pr ON s.productid = pr.productid
                            LEFT JOIN 
                                productcategories c ON pr.productcategoryid = c.productcategoryid
                            WHERE 
                               (CONVERT(DATE, b.bookdate) = CONVERT(DATE, GETDATE()) AND b.status = 'success') OR CONVERT(DATE, s.SaleDate) = CONVERT(DATE, GETDATE())

                            ORDER BY 
                               b.bookdate";
                SqlConnection con = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
                con.Open();
                var cmd = new SqlCommand(query, con);
                SqlDataReader temp = cmd.ExecuteReader();
                var reports = new List<object>();
                while (temp.Read())
                {
                    var time = (DateTime)temp["todaytime"];
                    var report = new
                    {
                        TodayTime = time.ToShortTimeString(),
                        BookingAmount = (double)temp["bookingamount"],
                        BarAmount = (double)temp["drinkamount"],
                        RestaurantAmount = (double)temp["foodamount"],
                    };
                    reports.Add(report);
                }
                con.Close();
                return reports;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<object>> GetGeneralDailyReport(DateTime from, DateTime to)
        {
            try
            {

                var query = @$"SELECT 
                                SUM(p.paidamount) AS bookingamount,
                                SUM(CASE WHEN c.categoryname = 'Food' THEN s.costprice * s.quantity ELSE 0 END) AS foodamount,
                                SUM(CASE WHEN c.categoryname = 'Drink' THEN s.costprice * s.quantity ELSE 0 END) AS drinkamount,
                                DATENAME(WEEKDAY, b.bookdate) AS bookingday
                                FROM 
                                    bookings b
                                JOIN 
                                    payments p ON b.bookingid = p.bookingid
                                LEFT JOIN 
                                    sales s ON s.saledate = b.bookdate
                                LEFT JOIN 
                                    products pr ON s.productid = pr.productid
                                LEFT JOIN 
                                    productcategories c ON pr.productcategoryid = c.productcategoryid
                                WHERE 
                                    (CONVERT(DATE, b.bookdate) >= '{from}' AND CONVERT(DATE, b.bookdate) <= '{to}' AND b.status = 'success')  OR
                                    CONVERT(DATE, S.saledate) >= '{from}' AND CONVERT(DATE, s.saledate) <= '{to}'
                                GROUP BY 
                                    DATENAME(WEEKDAY, b.bookdate)
                                ORDER BY 
                                    MIN(b.bookdate)";
                SqlConnection con = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
                con.Open();
                var cmd = new SqlCommand(query, con);
                SqlDataReader temp = cmd.ExecuteReader();
                var reports = new List<object>();
                while (temp.Read())
                {
                    var report = new
                    {
                        BookingDay = (string)temp["bookingday"],
                        BookingAmount = (double)temp["bookingamount"],
                        BarAmount = (double)temp["drinkamount"],
                        RestaurantAmount = (double)temp["foodamount"],
                    };
                    reports.Add(report);
                }
                con.Close();
                return reports;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<object>> GetGeneralWeeklyReport(DateTime from, DateTime to)
        {
            try
            {

                var query = @$"SELECT 
	                            SUM(p.paidamount) AS bookingamount,
                                SUM(CASE WHEN c.categoryname = 'Food' THEN s.costprice * s.quantity ELSE 0 END) AS foodamount,
                                SUM(CASE WHEN c.categoryname = 'Drink' THEN s.costprice * s.quantity ELSE 0 END) AS drinkamount,
	                            DATEPART(WEEK, b.bookdate) AS weeknumber
                                FROM 
                                    bookings b
                                JOIN 
                                    payments p ON b.bookingid = p.bookingid
                                LEFT JOIN 
                                    sales s ON s.saledate = b.bookdate
                                LEFT JOIN 
                                    products pr ON s.productid = pr.productid
                                LEFT JOIN 
                                    productcategories c ON pr.productcategoryid = c.productcategoryid
                                WHERE 
                                    (CONVERT(DATE, b.bookdate) >= '{from}' AND CONVERT(DATE, b.bookdate) <= '{to}' AND b.status = 'success')  OR
                                    CONVERT(DATE, S.saledate) >= '{from}' AND CONVERT(DATE, s.saledate) <= '{to}'
                                GROUP BY 
                                    DATEPART(WEEK, b.bookdate)
                                ORDER BY 
                                    weeknumber";
                SqlConnection con = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
                con.Open();
                var cmd = new SqlCommand(query, con);
                SqlDataReader temp = cmd.ExecuteReader();
                var reports = new List<object>();
                while (temp.Read())
                {
                    var report = new
                    {
                        WeekNumber = (int)temp["weeknumber"],
                        BookingAmount = (double)temp["bookingamount"],
                        BarAmount = (double)temp["drinkamount"],
                        RestaurantAmount = (double)temp["foodamount"],
                    };
                    reports.Add(report);
                }
                con.Close();
                return reports;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<object>> GetGeneralMonthlyReport(DateTime from, DateTime to)
        {
            try
            {

                var query = @$"SELECT 
                                SUM(p.paidamount) AS bookingamount,
                                SUM(CASE WHEN c.categoryname = 'Food' THEN s.costprice * s.quantity ELSE 0 END) AS foodamount,
                                SUM(CASE WHEN c.categoryname = 'Drink' THEN s.costprice * s.quantity ELSE 0 END) AS drinkamount,
                                DATENAME(MONTH, DATEADD(MONTH, DATEDIFF(MONTH, 0, b.bookdate), 0)) AS bookingmonth
                                FROM 
                                    bookings b
                                JOIN 
                                    payments p ON b.bookingid = p.bookingid
                                LEFT JOIN 
                                    sales s ON s.saledate = b.bookdate
                                LEFT JOIN 
                                    products pr ON s.productid = pr.productid
                                LEFT JOIN 
                                    productcategories c ON pr.productcategoryid = c.productcategoryid
                                WHERE 
                                    (CONVERT(DATE, b.bookdate) >= '{from}' AND CONVERT(DATE, b.bookdate) <= '{to}' AND b.status = 'success')  OR
                                    CONVERT(DATE, S.saledate) >= '{from}' AND CONVERT(DATE, s.saledate) <= '{to}'
                                GROUP BY 
                                    DATENAME(MONTH, DATEADD(MONTH, DATEDIFF(MONTH, 0, b.bookdate), 0))
                                ORDER BY 
                                    bookingmonth";
                SqlConnection con = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
                con.Open();
                var cmd = new SqlCommand(query, con);
                SqlDataReader temp = cmd.ExecuteReader();
                var reports = new List<object>();
                while (temp.Read())
                {
                    var report = new
                    {
                        MonthName = (string)temp["bookingmonth"],
                        BookingAmount = (double)temp["bookingamount"],
                        BarAmount = (double)temp["drinkamount"],
                        RestaurantAmount = (double)temp["foodamount"],
                    };
                    reports.Add(report);
                }
                con.Close();
                return reports;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<object>> GetGeneralYearlyReport(DateTime from, DateTime to)
        {
            try
            {

                var query = @$"SELECT 
                                SUM(p.paidamount) AS bookingamount,
                                SUM(CASE WHEN c.categoryname = 'Food' THEN s.costprice * s.quantity ELSE 0 END) AS foodamount,
                                SUM(CASE WHEN c.categoryname = 'Drink' THEN s.costprice * s.quantity ELSE 0 END) AS drinkamount,
                                YEAR(b.bookdate) AS bookingyear
                                FROM 
                                    bookings b
                                JOIN 
                                    payments p ON b.bookingid = p.bookingid
                                LEFT JOIN 
                                    sales s ON s.saledate = b.bookdate
                                LEFT JOIN 
                                    products pr ON s.productid = pr.productid
                                LEFT JOIN 
                                    productcategories c ON pr.productcategoryid = c.productcategoryid
                                WHERE 
                                    (CONVERT(DATE, b.bookdate) >= '{from}' AND CONVERT(DATE, b.bookdate) <= '{to}' AND b.status = 'success')  OR
                                    CONVERT(DATE, S.saledate) >= '{from}' AND CONVERT(DATE, s.saledate) <= '{to}'
                                GROUP BY 
                                    YEAR(b.bookdate)
                                ORDER BY 
                                    bookingyear";
                SqlConnection con = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
                con.Open();
                var cmd = new SqlCommand(query, con);
                SqlDataReader temp = cmd.ExecuteReader();
                var reports = new List<object>();
                while (temp.Read())
                {
                    var report = new
                    {
                        YearNumber = (int)temp["bookingyear"],
                        BookingAmount = (double)temp["bookingamount"],
                        BarAmount = (double)temp["drinkamount"],
                        RestaurantAmount = (double)temp["foodamount"],
                    };
                    reports.Add(report);
                }
                con.Close();
                return reports;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
