namespace HotelManagementSystem.Areas.Management.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required(ErrorMessage ="the customer name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "the customer home address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "the customer phone number is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "the customer email is required")]
        public string Email { get; set; }
    }
}
