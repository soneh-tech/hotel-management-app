using HotelManagementSystem.Areas.Management.Models;
using HotelManagementSystem.Areas.Public.Models;
using HotelManagementSystem.Repositories.Management;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Runtime.CompilerServices;

namespace HotelManagementSystem.Services.Management
{
    public class CMSService : ICMSRepository
    {
        private readonly AppDbContext context;

        public CMSService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddEvent(Event @event)
        {
            await context.Events.AddAsync(@event);
            await context.SaveChangesAsync();
        }

        public async Task AddOffer(Offer offer)
        {

            await context.Offers.AddAsync(offer);
            await context.SaveChangesAsync();
        }

        public async Task AddSlider(Slider slider)
        {
            await context.Sliders.AddAsync(slider);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAboutGallery(int id)
        {
            var GalleryToDelete = await context.AboutGalleries.FindAsync(id);
            if (GalleryToDelete != null)
            {
                context.AboutGalleries.Remove(GalleryToDelete);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteEvent(int id)
        {
            var EventToDelete = await context.Events.FindAsync(id);
            if (EventToDelete != null)
            {
                context.Events.Remove(EventToDelete);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteOffer(int id)
        {
            var OfferToDelete = await context.Offers.FindAsync(id);
            if (OfferToDelete != null)
            {
                context.Offers.Remove(OfferToDelete);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteSlider(int id)
        {
            var SliderToDelete = await context.Sliders.FindAsync(id);
            if (SliderToDelete != null)
            {
                context.Sliders.Remove(SliderToDelete);
            }
            await context.SaveChangesAsync();
        }

        public async Task<About> GeAbout()
        {
            var result = await context.Abouts.FirstOrDefaultAsync();
            return result;
        }

        public async Task<AboutGallery> GetAboutGallery(int id)
        {
            var result = await context.AboutGalleries.FindAsync(id);
            return result;
        }

        public async Task<AboutService> GetAboutService(int id)
        {
            var result = await context.AboutServices.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<AboutService>> GetAboutServices()
              => await context.AboutServices.ToListAsync();


        public async Task<Contact> GetContact()
        {
            var result = await context.Contacts.FirstOrDefaultAsync();
            return result;

        }

        public async Task<Event> GetEvent(int id)
        {
            var result = await context.Events.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Event>> GetEvents()
                  => await context.Events.ToListAsync();


        public async Task<IEnumerable<AboutGallery>> GetGalleries()
             => await context.AboutGalleries.ToListAsync();


        public async Task<Home> GetHome()
        {
            var result = await context.Homes.FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<HomeAboutImages>> GetHomeImages()
               => await context.HomeAboutImages.ToListAsync();


        public async Task<IEnumerable<HomeService>> GetHomeServices()
        => await context.HomeServices.ToListAsync();

        public async Task<Offer> GetOffer(int id)
        {
            var result = await context.Offers.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Offer>> GetOffers()
         => await context.Offers.ToListAsync();


        public async Task<IEnumerable<Slider>> GetSliders()
           => await context.Sliders.ToListAsync();


        public async Task<IEnumerable<SocialMedia>> GetSocialMedias()
          => await context.SocialMedias.ToListAsync();


        public async Task ModifyAbout(About about)
        {
            context.Abouts.Update(about);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAboutGallery(AboutGallery gallery)
        {
            context.AboutGalleries.Update(gallery);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAboutService(AboutService service)
        {
            context.AboutServices.Update(service);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAboutServices(IEnumerable<AboutService> aboutServices)
        {
            context.AboutServices.UpdateRange(aboutServices);
            await context.SaveChangesAsync();
        }

        public async Task ModifyContact(Contact contact)
        {
            context.Contacts.Update(contact);
            await context.SaveChangesAsync();
        }

        public async Task ModifyEvent(Event events)
        {
            context.Events.UpdateRange(events);
            await context.SaveChangesAsync();
        }

        public async Task ModifyGallery(IEnumerable<AboutGallery> aboutGalleries)
        {
            context.AboutGalleries.UpdateRange(aboutGalleries);
            await context.SaveChangesAsync();
        }

        public async Task ModifyHome(Home home)
        {
            context.Homes.Update(home);
            await context.SaveChangesAsync();
        }

        public async Task ModifyHomeImages(IEnumerable<HomeAboutImages> images)
        {
            context.HomeAboutImages.UpdateRange(images);
            await context.SaveChangesAsync();
        }

        public async Task ModifyHomeServices(IEnumerable<HomeService> services)
        {
            context.HomeServices.UpdateRange(services);
            await context.SaveChangesAsync();
        }

        public async Task ModifyOffer(IEnumerable<Offer> offers)
        {
            context.Offers.UpdateRange(offers);
            await context.SaveChangesAsync();
        }

        public async Task ModifySocialMedias(IEnumerable<SocialMedia> socialMedias)
        {
            context.SocialMedias.UpdateRange(socialMedias);
            await context.SaveChangesAsync();
        }
    }
}
