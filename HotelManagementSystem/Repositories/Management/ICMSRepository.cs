
namespace HotelManagementSystem.Repositories.Management
{
    public interface ICMSRepository
    {
        Task<Home> GetHome();
        Task ModifyHome(Home home);
        Task<About> GeAbout();
        Task ModifyAbout(About about);
        Task<Contact> GetContact();
        Task ModifyContact(Contact contact);
        Task<AboutGallery> GetAboutGallery(int id);
        Task DeleteAboutGallery(int id);
        Task ModifyAboutGallery(AboutGallery gallery);
        Task<AboutService> GetAboutService(int id);
        Task ModifyAboutService(AboutService service);

        Task<IEnumerable<AboutService>> GetAboutServices();
        Task ModifyAboutServices(IEnumerable<AboutService> aboutServices);
        Task<IEnumerable<AboutGallery>> GetGalleries();
        Task ModifyGallery(IEnumerable<AboutGallery> aboutGalleries);

        Task<IEnumerable<SocialMedia>> GetSocialMedias();
        Task ModifySocialMedias(IEnumerable<SocialMedia> socialMedias);
     
        Task AddOffer(Offer offer);
        Task<Offer> GetOffer(int id);
        Task DeleteOffer(int id);
        Task<IEnumerable<Offer>> GetOffers();
        Task ModifyOffer(IEnumerable<Offer> offers);

        Task<IEnumerable<Event>> GetEvents();
        Task ModifyEvent(Event events);
        Task AddEvent(Event @event);
        Task DeleteEvent(int id);
        Task<Event> GetEvent(int id);

        Task<IEnumerable<HomeService>> GetHomeServices();
        Task ModifyHomeServices(IEnumerable<HomeService> services); 
        Task<IEnumerable<HomeAboutImages>> GetHomeImages();
        Task ModifyHomeImages(IEnumerable<HomeAboutImages> images);

        Task<IEnumerable<Slider>> GetSliders();
        Task AddSlider(Slider slider);
        Task DeleteSlider (int id);

    }
}
