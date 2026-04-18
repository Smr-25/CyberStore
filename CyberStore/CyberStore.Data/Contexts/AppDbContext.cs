using Microsoft.EntityFrameworkCore;
using CyberStore.Core.Entities;

namespace CyberStore.Data.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<WishlistItem> WishlistItems { get; set; } = null!;
    public DbSet<ContactMessage> ContactMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Define some basic constraints or seed data if needed
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<Product>()
            .Property(p => p.OldPrice)
            .HasColumnType("decimal(18,2)");

        // Seed Data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Laptops & Tablets", Icon = "fa-laptop" },
            new Category { Id = 2, Name = "Smartphones", Icon = "fa-mobile" },
            new Category { Id = 3, Name = "Accessories", Icon = "fa-headphones" }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Apple", LogoUrl = "" },
            new Brand { Id = 2, Name = "Samsung", LogoUrl = "" },
            new Brand { Id = 3, Name = "Sony", LogoUrl = "" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Apple MacBook Pro 13\"", Description = "With Touch Bar MF841DSN/A", Price = 1259.00m, OldPrice = null, ImageUrl = "./assets/img/h1-product-1.jpg", StockQuantity = 10, CategoryId = 1, BrandId = 1, IsNew = true },
            new Product { Id = 2, Name = "Ultra Wireless S50 Headphones", Description = "With Bluetooth", Price = 349.00m, OldPrice = null, ImageUrl = "./assets/img/h2-product-10.jpg", StockQuantity = 20, CategoryId = 3, BrandId = 3, IsNew = false },
            new Product { Id = 3, Name = "Apple iPhone 14 Pro", Description = "128GB Grey", Price = 999.00m, OldPrice = 1099.00m, ImageUrl = "./assets/img/h1-product-3.jpg", StockQuantity = 15, CategoryId = 2, BrandId = 1, IsNew = true }
        );
    }
}
