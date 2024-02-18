
namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class RoomTypeViewModel
    {
        public int RoomTypeID { get; set; }

        [Required(ErrorMessage = "the room type is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "the room size is required")]
        public int Size { get; set; }
        [Required(ErrorMessage = "the room capacity is required")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "the room price is required")]
        public int Price { get; set; }
        public List<IFormFile>? Photo { get; set; }
        public string? ExixtingPhoto { get; set; }
        public ICollection<Ammenity>? Ammenities { get; set; }   
    }
}
