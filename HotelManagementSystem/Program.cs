global using Microsoft.Data.SqlClient;
global using HotelManagementSystem.Areas.Management.Models;
global using HotelManagementSystem.Areas.Public.Models;
global using Microsoft.AspNetCore.Mvc;
global using HotelManagementSystem.Areas.Management.ViewModels;
global using HotelManagementSystem.Areas.Public.ViewModels;
global using System.ComponentModel.DataAnnotations;
global using HotelManagementSystem.Data;
global using Microsoft.EntityFrameworkCore;
global using HotelManagementSystem.Repositories.Management;
global using HotelManagementSystem.Services.Management;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>
    (o =>
   {
       o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
   });

builder.Services.AddIdentity<AppUser, IdentityRole>( o=> { o.SignIn.RequireConfirmedEmail = true; })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(o => { o.LoginPath = "/Management/Account/Index"; });
builder.Services.AddScoped<IManagementRepository, ManagementService>();
builder.Services.AddScoped<ICMSRepository, CMSService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
  //pattern: "{area=Public}/{controller=Home}/{action=Index}/{id?}");
pattern: "{area=Public}/{controller=Home}/{action=Index}");


app.Run();

