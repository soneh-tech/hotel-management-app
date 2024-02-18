namespace HotelManagementSystemAPI.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string? ItemName { get; set; }
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public int ItemCategoryID { get; set; }
    }
}
