namespace HotelManagementSystem.Areas.Management.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "the product name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "the product price is required")]
        public double UnitPrice { get; set; }
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "the product Category is required")]
        public int ProductCategoryID { get; set; }
        public IEnumerable<Sale>? Sales { get; set; }
    }
}
