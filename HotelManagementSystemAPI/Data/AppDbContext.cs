using HotelManagementSystemAPI.Models;
using HotelManagementSystemAPI.Models.C_Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ammenity> Ammenities { get; set;}
        public DbSet<Booking> Bookings { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Designation> Designations { get; set;}
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Item> Items { get; set;}
        public DbSet<ItemCategory> ItemCategories { get; set;}
        public DbSet<Payment> Payments { get; set;}
        public DbSet<Review> Reviews { get; set;}
        public DbSet<Room> Rooms { get; set;}
        public DbSet<RoomType> RoomTypes { get; set;}
        public DbSet<Sale> Sales { get; set;}
        public DbSet<AboutModel> AboutModels { get; set;}
        public DbSet<ContactModel> ContactModels { get; set;}
        public DbSet<GeneralModel> GeneralModels { get; set;}
        public DbSet<HomeModel> homeModels { get; set;}
        public DbSet<NewsModel> NewsModels { get; set;}
        public DbSet<OfferModel> OfferModels { get; set;}
        public DbSet<ServiceModel> ServiceModels { get; set;}
        public DbSet<SliderModel> SliderModels { get; set;}
        public DbSet<SocialMedisModel> SocialMedisModels { get; set;}

    }
}
