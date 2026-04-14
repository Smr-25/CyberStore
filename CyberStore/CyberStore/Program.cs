using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add appsettings.Mac.json
builder.Configuration.AddJsonFile("appsettings.Mac.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Database
builder.Services.AddDbContext<CyberStore.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped(typeof(CyberStore.Repositories.Interfaces.IGenericRepository<>), typeof(CyberStore.Repositories.GenericRepository<>));
builder.Services.AddScoped<CyberStore.Repositories.Interfaces.IProductRepository, CyberStore.Repositories.ProductRepository>();
builder.Services.AddScoped<CyberStore.Repositories.Interfaces.IUnitOfWork, CyberStore.Repositories.UnitOfWork>();

// Register Services (Business Logic)
builder.Services.AddScoped<CyberStore.Services.Interfaces.IProductService, CyberStore.Services.ProductService>();
builder.Services.AddScoped<CyberStore.Services.Interfaces.ICartService, CyberStore.Services.CartService>();
builder.Services.AddScoped<CyberStore.Services.Interfaces.IWishlistService, CyberStore.Services.WishlistService>();
builder.Services.AddScoped<CyberStore.Services.Interfaces.IContactService, CyberStore.Services.ContactService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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