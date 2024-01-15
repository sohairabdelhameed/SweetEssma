using EssmaCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetEssma.Models;
using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SweetEssmaDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SweetEssmaDbContextConnection' not found.");

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options=>{
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddRazorPages();


builder.Services.AddScoped<ICategoryRepository, CategoryReprository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp=>ShoppingCart.GetCart(sp));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<SweetEssmaDbContext>(
    options => {
        options.UseSqlServer(
            builder.Configuration["ConnectionStrings:SweetEssmaDbContextConnection"]);
    });

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<SweetEssmaDbContext>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Error");
app.UseHsts();
}

app.UseHttpsRedirection();
app.MapRazorPages();

DbInitializer.Seed(app);
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

//app.MapGet("/hi", () => "Hello!");

app.MapDefaultControllerRoute(); //{controller=Home}/{action=Index}/{id?}
//app.MapControllerRoute(
//name: "defualt",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//    );


app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
////builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
//builder.Services.AddScoped<IPieRepository, MockPieRepository>();
////IServiceCollection.AddControllers;
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage(); // it will show us our error,it may contain the secret information

//    //app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    //app.UseHs ts();
//}
////middleware components
////app.UseHttpsRedirection();

////MapDefaultControllerRoute();
//app.UseStaticFiles();
////it will let MVC handel incoming requests on controllers
//app.UseRouting();

////app.UseAuthorization();

//app.MapControllerRoute(
//name: "default",
//   pattern: "{controller= Pie}/{ action= List}/{id?}");

//app.Run(); //start our application
