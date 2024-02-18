using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class PaymentViewModel:Payment
    {
        public int Price { get; set; }
    }
}
