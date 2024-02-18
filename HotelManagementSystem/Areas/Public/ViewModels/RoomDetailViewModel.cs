namespace HotelManagementSystem.Areas.Public.ViewModels
{
    public class RoomDetailViewModel
    {
        public Contact contact { get; set; }
        public Booking booking { get; set; }
        public RoomType room { get; set; }
        public Review review { get; set; }
        public IEnumerable<Review> reviews { get; set; }
        public IEnumerable<SocialMedia> socialMedias { get; set; }
    }
}
