using CyberStore.Core.Entities;
using CyberStore.Data.Repositories.Interfaces;
using CyberStore.Application.Services.Interfaces;

namespace CyberStore.Application.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
    {
        var items = await _unitOfWork.CartItems.FindAsync(c => c.UserId == userId);
        
        // Load products for each cart item
        foreach(var item in items)
        {
            item.Product = await _unitOfWork.Products.GetByIdAsync(item.ProductId) ?? new Product();
        }
        
        return items;
    }

    public async Task AddToCartAsync(string userId, int productId, int quantity)
    {
        var existingItems = await _unitOfWork.CartItems.FindAsync(c => c.UserId == userId && c.ProductId == productId);
        var existingItem = existingItems.FirstOrDefault();

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            _unitOfWork.CartItems.Update(existingItem);
        }
        else
        {
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                DateAdded = DateTime.UtcNow
            };
            await _unitOfWork.CartItems.AddAsync(cartItem);
        }

        await _unitOfWork.CompleteAsync();
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var cartItem = await _unitOfWork.CartItems.GetByIdAsync(cartItemId);
        if (cartItem != null)
        {
            _unitOfWork.CartItems.Remove(cartItem);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task ClearCartAsync(string userId)
    {
        var items = await _unitOfWork.CartItems.FindAsync(c => c.UserId == userId);
        foreach (var item in items)
        {
            _unitOfWork.CartItems.Remove(item);
        }
        await _unitOfWork.CompleteAsync();
    }
}
