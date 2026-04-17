using CyberStore.Core.Entities;
using CyberStore.Data.Repositories.Interfaces;
using CyberStore.Application.Services.Interfaces;

namespace CyberStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _unitOfWork.Products.GetProductsWithDetailsAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _unitOfWork.Products.GetProductWithDetailsAsync(id);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _unitOfWork.Products.GetProductsByCategoryAsync(categoryId);
    }
}
