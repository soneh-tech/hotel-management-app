

namespace HotelManagementSystem.Areas.Management.Models
{
    public class Ammenity
    {
        public int AmmenityID { get; set; }
        [Required(ErrorMessage ="the ammenity name must be specified")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please specify if the ammenity is currently available")]
        public bool IsActive { get; set; }
        public ICollection<RoomType>? RoomTypes { get; set; }

    }

}