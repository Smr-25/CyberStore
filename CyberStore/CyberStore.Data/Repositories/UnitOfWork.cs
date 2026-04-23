using CyberStore.Data.Contexts;
using CyberStore.Data.Repositories.Interfaces;

namespace CyberStore.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IProductRepository Products { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.Category> Categories { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.Brand> Brands { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.CartItem> CartItems { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.WishlistItem> WishlistItems { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.ContactMessage> ContactMessages { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.Order> Orders { get; private set; }
    public IGenericRepository<CyberStore.Core.Entities.OrderItem> OrderItems { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Categories = new GenericRepository<CyberStore.Core.Entities.Category>(_context);
        Brands = new GenericRepository<CyberStore.Core.Entities.Brand>(_context);
        CartItems = new GenericRepository<CyberStore.Core.Entities.CartItem>(_context);
        WishlistItems = new GenericRepository<CyberStore.Core.Entities.WishlistItem>(_context);
        ContactMessages = new GenericRepository<CyberStore.Core.Entities.ContactMessage>(_context);
        Orders = new GenericRepository<CyberStore.Core.Entities.Order>(_context);
        OrderItems = new GenericRepository<CyberStore.Core.Entities.OrderItem>(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
