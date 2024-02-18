namespace HotelManagementSystem.Areas.Public.ViewModels
{
    public class RoomsViewModel
    {
        public IEnumerable<RoomType> rooms { get; set; }
        public IEnumerable<SocialMedia> socialMedias { get; set; }
        public Contact contact { get; set; }
    }
}
