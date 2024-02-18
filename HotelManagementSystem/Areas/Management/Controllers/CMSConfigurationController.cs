using HotelManagementSystem.Areas.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.Extensions.Hosting.Internal;
using static PayStack.Net.CustomerList;

namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CMSConfigurationController : Controller
    {
        private readonly ICMSRepository _repository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public CMSConfigurationController(ICMSRepository repository, IWebHostEnvironment hostingEnvironment)
        {
            _repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Home(HomeViewModel model)
        {
            model.home = await _repository.GetHome();
            model.home_sliders = await _repository.GetSliders();
            model.home_aboutImages = await _repository.GetHomeImages();

            model.services = await _repository.GetHomeServices();
            model.home_aboutImages = await _repository.GetHomeImages();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditHome(HomeViewModel model, List<HomeService> homeServices)
        {
            //update records
            await _repository.ModifyHome(model.home);
            await _repository.ModifyHomeServices(homeServices.AsEnumerable());
            string uniqueFile;
            if (model.aboutImages != null && model.aboutImages.Count > 0)
            {
                if (model.home_aboutImages != null)
                {
                    foreach (var item in model.home_aboutImages)
                    {
                        if (item != null)
                        {
                            string filepath =
                                Path.Combine(hostingEnvironment.WebRootPath, "images/abouts", item.ImageURL);
                            System.IO.File.Delete(filepath);
                        }
                    }
                }

                var home_aboutImages = new List<HomeAboutImages>();
                int id = 0;
                foreach (var photo in model.aboutImages)
                {
                    id += 1;
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/abouts");
                    uniqueFile = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFile);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    { await photo.CopyToAsync(filestream); }

                    var image = new HomeAboutImages { ID = id, ImageURL = uniqueFile };
                    home_aboutImages.Add(image);

                }

                await _repository.ModifyHomeImages(home_aboutImages.AsEnumerable());

            }
            string sliderFile;
            if (model.sliders != null && model.sliders.Count > 0)
            {

                foreach (var photo in model.sliders)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/sliders");
                    sliderFile = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, sliderFile);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    { await photo.CopyToAsync(filestream); }

                    var image = new Slider { ImageURL = sliderFile };
                    await _repository.AddSlider(image);
                }


            }


            //return home
            model.home = await _repository.GetHome();
            model.home_sliders = await _repository.GetSliders();
            model.home_aboutImages = await _repository.GetHomeImages();
            model.services = await _repository.GetHomeServices();
            model.home_aboutImages = await _repository.GetHomeImages();
            return View("Home", model);
        }



        [HttpGet]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            model.contact = await _repository.GetContact();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditContact(ContactViewModel model)
        {
            await _repository.ModifyContact(model.contact);
            return View("Contact", model);
        }



        [HttpGet]
        public async Task<IActionResult> SocialMedia(IEnumerable<SocialMedia> model)
        {
            model = await _repository.GetSocialMedias();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditMedia(List<SocialMedia> model)
        {
            await _repository.ModifySocialMedias(model.AsEnumerable());
            return View("SocialMedia", model.AsEnumerable());
        }




        [HttpGet]
        public async Task<IActionResult> AboutMain(AboutViewModel model)
        {
            model.about = await _repository.GeAbout();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAboutMain(AboutViewModel model)
        {
            await _repository.ModifyAbout(model.about);
            return RedirectToAction("AboutMain");
        }


        [HttpGet]
        public async Task<IActionResult> AboutGalleries(AboutViewModel model)
        {
            model.galleries = await _repository.GetGalleries();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAboutGalleries(AboutViewModel model)
        {
            var image_url = model.gallery.ImageURL;
            if (model.image != null)
            {
                if (model.gallery.ImageURL != null)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                        "images/gallery", model.gallery.ImageURL);
                    System.IO.File.Delete(filepath);

                }

                image_url = await ProcessPhoto(model);

            }
            var gallery = new AboutGallery { ID = model.gallery.ID, Title = model.gallery.Title, ImageURL = image_url };

            await _repository.ModifyAboutGallery(gallery);
            return RedirectToAction("AboutGalleries");
        }
        [HttpGet]
        public async Task<IActionResult> GetGallery(int id)
        {
            var result = await _repository.GetAboutGallery(id);
            var item = new AboutViewModel { gallery = result };
            return PartialView(item);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGallery(AboutViewModel model)
        {
            await _repository.DeleteAboutGallery(model.gallery.ID);
            return RedirectToAction("AboutGalleries");
        }
        private async Task<string> ProcessPhoto(AboutViewModel model)
        {
            string uniqueFile;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/gallery");
            uniqueFile = Guid.NewGuid().ToString() + "_" + model.image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFile);
            using (var filestream = new FileStream(filePath, FileMode.Create))
            { await model.image.CopyToAsync(filestream); }

            return uniqueFile;
        }
        private async Task<string> ProcessEventPhoto(EventsViewModel model)
        {
            string uniqueFile;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/news");
            uniqueFile = Guid.NewGuid().ToString() + "_" + model.image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFile);
            using (var filestream = new FileStream(filePath, FileMode.Create))
            { await model.image.CopyToAsync(filestream); }

            return uniqueFile;
        }



        [HttpGet]
        public async Task<IActionResult> AboutOffers(AboutViewModel model)
        {
            model.offers = await _repository.GetOffers();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAboutOffers(List<Offer> offers)
        {
            await _repository.ModifyOffer(offers.AsEnumerable());
            return RedirectToAction("AboutOffers");
        }
        [HttpPost]
        public async Task<IActionResult> AddOffer(Offer offer)
        {
            await _repository.AddOffer(offer);

            AboutViewModel model = new AboutViewModel();
            model.offers = await _repository.GetOffers();
            return View("AboutOffers", model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOffer(AboutViewModel model)
        {
            await _repository.DeleteOffer(model.offer.ID);
            return RedirectToAction("AboutOffers");
        }


        [HttpGet]
        public async Task<IActionResult> AboutServices(AboutViewModel model)
        {
            model.services = await _repository.GetAboutServices();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAboutServices(AboutViewModel model)
        {
            var image_url = model.service.ImageURL;
            if (model.image != null)
            {
                if (model.service.ImageURL != null)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                        "images/abouts", model.service.ImageURL);
                    System.IO.File.Delete(filepath);

                }

                image_url = await ProcessPhoto(model);

            }
            var service = new AboutService { ID = model.service.ID, Title = model.service.Title, ImageURL = image_url };

            await _repository.ModifyAboutService(service);
            return RedirectToAction("AboutServices");
        }
        [HttpGet]
        public async Task<IActionResult> GetService(int id)
        {
            var result = await _repository.GetAboutService(id);
            var item = new AboutViewModel { service = result };
            return PartialView(item);
        }




        [HttpGet]
        public async Task<IActionResult> News(EventsViewModel model)
        {
            model.events = await _repository.GetEvents();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _repository.GetEvent(id);
            var item = new EventsViewModel { my_event = result };
            return PartialView(item);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent(EventsViewModel model)
        {
            if (model.my_event.ID is 0)
            {
                var image_url = ""; Event @event;
                if (model.image != null)
                {
                    if (model.my_event.ImageURL != null)
                    {
                        string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images/news", model.my_event.ImageURL);
                        System.IO.File.Delete(filepath);

                    }
                    image_url = await ProcessEventPhoto(model);
                }


                @event = new Event { Title = model.my_event.Title, NewsDate = model.my_event.NewsDate, ImageURL = image_url };
                await _repository.AddEvent(@event);
            }
            else
            {
                var image_url = model.my_event.ImageURL; Event @event;
                if (model.image != null)
                {
                    if (model.my_event.ImageURL != null)
                    {
                        string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images/news", model.my_event.ImageURL);
                        System.IO.File.Delete(filepath);

                    }
                    image_url = await ProcessEventPhoto(model);
                }
                if (image_url is null)
                    image_url = string.Empty;
                @event = new Event { ID = model.my_event.ID, Title = model.my_event.Title, NewsDate = model.my_event.NewsDate, ImageURL = image_url };
                await _repository.ModifyEvent(@event);
            }
            model.events = await _repository.GetEvents();
            return PartialView("_EventPartialView", model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveEvent(EventsViewModel model)
        {
            if (model.my_event.ImageURL != null)
            {
                string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                     "images/news", model.my_event.ImageURL);
                System.IO.File.Delete(filepath);
            }
            await _repository.DeleteEvent(model.my_event.ID);
            model.events = await _repository.GetEvents();
            return PartialView("_EventPartialView", model);
        }

    }
}
