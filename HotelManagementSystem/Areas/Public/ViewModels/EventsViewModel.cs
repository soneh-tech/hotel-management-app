namespace HotelManagementSystem.Areas.Public.ViewModels
{
    public class EventsViewModel
    {
        public Contact contact { get; set; }
        public Event my_event { get; set; }
        public IEnumerable<Event> events { get; set; }
        public IEnumerable<SocialMedia> socialMedias { get; set; }
        public IFormFile? image { get; set; }
    }
}
