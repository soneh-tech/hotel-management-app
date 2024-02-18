using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Areas.Management.ViewModels
{
    public class SalesViewModel
    {
        public List<SelectListItem> customers { get; set; }
        public Sale Sale { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
    }
}
