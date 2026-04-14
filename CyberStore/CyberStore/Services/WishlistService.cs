using CyberStore.Models;
using CyberStore.Repositories.Interfaces;
using CyberStore.Services.Interfaces;

namespace CyberStore.Services;

public class WishlistService(IUnitOfWork unitOfWork) : IWishlistService
{
    public async Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(string userId)
    {
        var items = await unitOfWork.WishlistItems.FindAsync(w => w.UserId == userId);
        
        foreach (var item in items)
        {
            item.Product = await unitOfWork.Products.GetByIdAsync(item.ProductId) ?? new Product();
        }
        
        return items;
    }

    public async Task AddToWishlistAsync(string userId, int productId)
    {
        var existingItems = await unitOfWork.WishlistItems.FindAsync(w => w.UserId == userId && w.ProductId == productId);
        
        if (!existingItems.Any())
        {
            var item = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                DateAdded = DateTime.UtcNow
            };
            await unitOfWork.WishlistItems.AddAsync(item);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task RemoveFromWishlistAsync(int wishlistItemId)
    {
        var item = await unitOfWork.WishlistItems.GetByIdAsync(wishlistItemId);
        if (item != null)
        {
            unitOfWork.WishlistItems.Remove(item);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task ClearWishlistAsync(string userId)
    {
        var items = await unitOfWork.WishlistItems.FindAsync(w => w.UserId == userId);
        foreach (var item in items)
        {
            unitOfWork.WishlistItems.Remove(item);
        }
        await unitOfWork.CompleteAsync();
    }
}
