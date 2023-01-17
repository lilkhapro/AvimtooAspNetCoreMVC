using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAppVide.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WebAppPieDbContextConnection") ?? throw new InvalidOperationException("Connection string 'WebAppPieDbContextConnection' not found.");


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository >();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddRazorPages();

//update after adding authentification 
builder.Services.AddDbContext<WebAppPieDbContext>(option =>
    option.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<WebAppPieDbContext>();;

//ajout blazor
builder.Services.AddServerSideBlazor();
//api controllers
builder.Services.AddControllers();
var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
//add authentication
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MapDefaultControllerRoute();
app.MapRazorPages();

//ajoput blazor
app.MapBlazorHub();
app.UseRouting();
app.MapFallbackToPage("/app/{*catchall}", "/App/Index"); 

//api controllers
app.MapControllers();

DbInitializer.Seed(app);

app.Run();
