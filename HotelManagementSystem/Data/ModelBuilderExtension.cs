namespace HotelManagementSystem.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder model)
        {
            model.Entity<IdentityRole>().HasData(
                new IdentityRole { Id=Guid.NewGuid().ToString(),Name="Admin",NormalizedName="ADMIN".ToUpper()},
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Manager", NormalizedName = "MANAGER".ToUpper() },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Receptionist", NormalizedName = "RECEPTIONIST".ToUpper() },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Waiter", NormalizedName = "WAITER".ToUpper() }
                );
            model.Entity<ProductCategory>().HasData(
             new ProductCategory { ProductCategoryID = 1, CategoryName = "Food" },
             new ProductCategory { ProductCategoryID = 2, CategoryName = "Drink" }
             );
            model.Entity<SocialMedia>().HasData(
             new SocialMedia { ID = 1, Title = "Facebook", URL = "facebook.com" },
             new SocialMedia { ID = 2, Title = "Twitter", URL = "twitter.com" },
             new SocialMedia { ID = 3, Title = "Instagram", URL = "instagram.com" },
             new SocialMedia { ID = 4, Title = "Youtube", URL = "youtube.com" }
             );
            model.Entity<Contact>().HasData(
                new Contact { ID = 1, Phone = "08031930431", Email = "example@some.com", Address = "something", ShortDescription = "something" }
                );
            model.Entity<About>().HasData(
              new About { ID = 1, Description = "Built in 1910 during the Belle Epoque period, this hotel is located in the center of Paris, with easy access to the city’s tourist attractions. It offers tastefully decorated rooms", VideoTitle = "Discover Our Hotel & Services", VideoDescription = "It S Hurricane Season But We Are Visiting Hilton Head Island", VideoURL = "youtube.com" }
              );
            model.Entity<AboutService>().HasData(
            new AboutService { ID = 1, Title="Something", ImageURL = "Wallgate Apartments" },
            new AboutService { ID = 2, Title = "Something", ImageURL = "Wallgate Apartments" },
            new AboutService { ID = 3, Title = "Something", ImageURL = "Wallgate Apartments" }
            );
            model.Entity<AboutGallery>().HasData(
           new AboutGallery { ID = 1, Title = "Something", ImageURL = "Wallgate Apartments" },
           new AboutGallery { ID = 2, Title = "Something", ImageURL = "Wallgate Apartments" },
           new AboutGallery { ID = 3, Title = "Something", ImageURL = "Wallgate Apartments" }
           );
            model.Entity<Home>().HasData(
                new Home { ID = 1, Title = "Wallgate Apartments", Description = "Here are the best hotel booking sites, including recommendations for international travel and for finding low-priced hotel rooms", AboutTitle = "Wallgate\r\nApartments", AboutDescription = "wallgate.com is a leading online accommodation site. We’re passionate about travel. Every day, we inspire and reach millions of travelers across the 36 states in nigeria\r\n\r\nSo when it comes to booking the perfect hotel, vacation rental, apartment, or guest house, we’ve got you covered." }
                );
            model.Entity<HomeService>().HasData(
               new HomeService { ID = 1, Title = "Catering Service", Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna" },
               new HomeService { ID = 2, Title = "Laundry", Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna" },
               new HomeService { ID = 3, Title = "Bar & Drinks", Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna" }
               );
            model.Entity<HomeAboutImages>().HasData(
              new HomeAboutImages { ID = 1, ImageURL = "Wallgate Apartments" },
              new HomeAboutImages { ID = 2, ImageURL = "Wallgate Apartments" }
              );
            model.Entity<Offer>().HasData(
              new Offer { ID = 1, Title = "20% Off On Accommodation" },
              new Offer { ID = 2, Title = "Complimentary Daily Breakfast" },
              new Offer { ID = 3, Title = "3 Pcs Laundry Per Day" },
              new Offer { ID = 4, Title = "Free Wifi" },
              new Offer { ID = 5, Title = "Discount 20% On F&B" }
              );
            model.Entity<Event>().HasData(
              new Event { ID = 1, Title="something", NewsDate = DateTime.Now, ImageURL = "Wallgate Apartments" },
              new Event { ID = 2, Title = "something", NewsDate = DateTime.Now, ImageURL = "Wallgate Apartments" },
              new Event { ID = 3, Title = "something", NewsDate = DateTime.Now, ImageURL = "Wallgate Apartments" }
              );
        }
    }
}
