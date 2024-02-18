namespace HotelManagementSystemAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageURL { get; set; }
        public int DesignationID { get; set; }
    }
}
