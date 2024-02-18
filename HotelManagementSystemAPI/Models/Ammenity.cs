using HotelManagementSystemAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace HotelManagementSystemAPI.Models
{
    public class Ammenity
    {
        public int AmmenityID { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }

    }

}