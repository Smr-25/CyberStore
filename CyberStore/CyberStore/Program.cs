using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.Mac.json", optional: true, reloadOnChange: true);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<CyberStore.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped(typeof(CyberStore.Repositories.Interfaces.IGenericRepository<>), typeof(CyberStore.Repositories.GenericRepository<>));
builder.Services.AddScoped<CyberStore.Repositories.Interfaces.IProductRepository, CyberStore.Repositories.ProductRepository>();
builder.Services.AddScoped<CyberStore.Repositories.Interfaces.IUnitOfWork, CyberStore.Repositories.UnitOfWork>();


builder.Services.AddScoped<CyberStore.Services.Interfaces.IProductService, CyberStore.Services.ProductService>();
builder.Services.AddScoped<CyberStore.Services.Interfaces.ICartService, CyberStore.Services.CartService>();
builder.Services.AddScoped<CyberStore.Services.Interfaces.IWishlistService, CyberStore.Services.WishlistService>();
builder.Services.AddScoped<CyberStore.Services.Interfaces.IContactService, CyberStore.Services.ContactService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();