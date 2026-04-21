using CyberStore.Data.Contexts;
using CyberStore.Core.Entities;
using CyberStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CyberStore.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsWithDetailsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ToListAsync();
    }

    public async Task<Product?> GetProductWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await _dbSet
            .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ToListAsync();
    }
}
