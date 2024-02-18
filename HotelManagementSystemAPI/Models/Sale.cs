namespace HotelManagementSystemAPI.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime? SaleDate { get; set; }
        public int ItemID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
    }
}
