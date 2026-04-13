using CyberStore.Models;

namespace CyberStore.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsWithDetailsAsync();
    Task<Product?> GetProductWithDetailsAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
}
