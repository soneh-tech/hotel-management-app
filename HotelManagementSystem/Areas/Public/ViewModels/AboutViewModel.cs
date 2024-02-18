namespace HotelManagementSystem.Areas.Public.ViewModels
{
    public class AboutViewModel
    {
        public About about { get; set; }
        public Contact contact { get; set; }
        public AboutService service { get; set; }
        public AboutGallery gallery { get; set; }
        public Offer offer { get; set; }
        public IFormFile? image { get; set; }
        public IEnumerable<Offer> offers { get; set; }
        public IEnumerable<SocialMedia> socialMedias { get; set; }
        public IEnumerable<AboutService> services { get; set; }
        public IEnumerable<AboutGallery> galleries { get; set; }

    }
}
