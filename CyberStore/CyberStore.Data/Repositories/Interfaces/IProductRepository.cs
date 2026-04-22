using CyberStore.Core.Entities;

namespace CyberStore.Data.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsWithDetailsAsync();
    Task<Product?> GetProductWithDetailsAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<Product>> GetSortedProductsAsync(string sortBy = "name");
}
