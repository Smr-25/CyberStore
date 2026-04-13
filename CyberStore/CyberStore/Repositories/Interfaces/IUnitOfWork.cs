using CyberStore.Repositories.Interfaces;

namespace CyberStore.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IGenericRepository<Models.Category> Categories { get; }
    IGenericRepository<Models.Brand> Brands { get; }
    IGenericRepository<Models.CartItem> CartItems { get; }
    IGenericRepository<Models.WishlistItem> WishlistItems { get; }
    IGenericRepository<Models.ContactMessage> ContactMessages { get; }
    
    Task<int> CompleteAsync();
}
