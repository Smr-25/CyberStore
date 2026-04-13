using CyberStore.Data;
using CyberStore.Repositories.Interfaces;

namespace CyberStore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IProductRepository Products { get; private set; }
    public IGenericRepository<Models.Category> Categories { get; private set; }
    public IGenericRepository<Models.Brand> Brands { get; private set; }
    public IGenericRepository<Models.CartItem> CartItems { get; private set; }
    public IGenericRepository<Models.WishlistItem> WishlistItems { get; private set; }
    public IGenericRepository<Models.ContactMessage> ContactMessages { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Categories = new GenericRepository<Models.Category>(_context);
        Brands = new GenericRepository<Models.Brand>(_context);
        CartItems = new GenericRepository<Models.CartItem>(_context);
        WishlistItems = new GenericRepository<Models.WishlistItem>(_context);
        ContactMessages = new GenericRepository<Models.ContactMessage>(_context);
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
