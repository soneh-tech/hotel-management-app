namespace HotelManagementSystem.Areas.Public.ViewModels
{
    public class HomeViewModel
    {
        public Home home { get; set; }
        public List<IFormFile>? sliders { get; set; }
        public List<IFormFile>? aboutImages { get; set; }
        public IEnumerable<Slider> home_sliders { get; set; }
        public IEnumerable<HomeAboutImages> home_aboutImages { get; set; }
        public IEnumerable<HomeService> services { get; set; }
        public IEnumerable<SocialMedia> socialMedias { get; set; }
        public Contact contact { get; set; }
        public IEnumerable<RoomType> rooms { get; set; }
        public List<Review> reviews { get; set; }
        public Booking booking { get; set; }

    }
}
