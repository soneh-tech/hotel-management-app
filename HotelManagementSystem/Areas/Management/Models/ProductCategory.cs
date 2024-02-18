namespace HotelManagementSystem.Areas.Management.Models
{
    public class ProductCategory
    {
        public int ProductCategoryID { get; set; }
        [Required(ErrorMessage = "the category name is required")]
        public string CategoryName { get; set; }
        public IEnumerable<Product>? Products { get;}
    }
}
