using CyberStore.Data.Repositories.Interfaces;

namespace CyberStore.Data.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IGenericRepository<CyberStore.Core.Entities.Category> Categories { get; }
    IGenericRepository<CyberStore.Core.Entities.Brand> Brands { get; }
    IGenericRepository<CyberStore.Core.Entities.CartItem> CartItems { get; }
    IGenericRepository<CyberStore.Core.Entities.WishlistItem> WishlistItems { get; }
    IGenericRepository<CyberStore.Core.Entities.ContactMessage> ContactMessages { get; }
    IGenericRepository<CyberStore.Core.Entities.Order> Orders { get; }
    IGenericRepository<CyberStore.Core.Entities.OrderItem> OrderItems { get; }
    
    Task<int> CompleteAsync();
}
