using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.Mac.json", optional: true, reloadOnChange: true);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<CyberStore.Data.Contexts.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped(typeof(CyberStore.Data.Repositories.Interfaces.IGenericRepository<>), typeof(CyberStore.Data.Repositories.GenericRepository<>));
builder.Services.AddScoped<CyberStore.Data.Repositories.Interfaces.IProductRepository, CyberStore.Data.Repositories.ProductRepository>();
builder.Services.AddScoped<CyberStore.Data.Repositories.Interfaces.IUnitOfWork, CyberStore.Data.Repositories.UnitOfWork>();


builder.Services.AddScoped<CyberStore.Application.Services.Interfaces.IProductService, CyberStore.Application.Services.ProductService>();
builder.Services.AddScoped<CyberStore.Application.Services.Interfaces.ICartService, CyberStore.Application.Services.CartService>();
builder.Services.AddScoped<CyberStore.Application.Services.Interfaces.IWishlistService, CyberStore.Application.Services.WishlistService>();
builder.Services.AddScoped<CyberStore.Application.Services.Interfaces.IContactService, CyberStore.Application.Services.ContactService>();

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