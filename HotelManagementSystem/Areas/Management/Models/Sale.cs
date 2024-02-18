namespace HotelManagementSystem.Areas.Management.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int? CustomerID { get; set; }
        public string? EmployeeID { get; set; }
        public int Quantity { get; set; }
        public double CostPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public  Product? Product  {get;set;}
        public Customer? Customer { get; set; }
        public AppUser? Employee { get; set; }

    }
}
