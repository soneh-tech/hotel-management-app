
namespace HotelManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            foreach(var foreignKey in builder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict; 
            }
            base.OnModelCreating(builder);
        }
        public DbSet<Ammenity> Ammenities { get; set;}
        public DbSet<Booking> Bookings { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<ProductCategory> ProductCategories { get; set;}
        public DbSet<Payment> Payments { get; set;}
        public DbSet<Review> Reviews { get; set;}
        public DbSet<Room> Rooms { get; set;}
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Sale> Sales { get; set;}
        public DbSet<About> Abouts { get; set;}
        public DbSet<Contact> Contacts { get; set;}
        public DbSet<Home> Homes { get; set;}
        public DbSet<Event> Events { get; set;}
        public DbSet<Offer> Offers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<AboutService> AboutServices { get; set; }
        public DbSet<AboutGallery> AboutGalleries { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }
        public DbSet<HomeAboutImages> HomeAboutImages { get; set; }
        public DbSet<Slider> Sliders { get; set;}
    }
}
