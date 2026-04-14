using CyberStore.Models;

namespace CyberStore.Services.Interfaces;

public interface IWishlistService
{
    Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(string userId);
    Task AddToWishlistAsync(string userId, int productId);
    Task RemoveFromWishlistAsync(int wishlistItemId);
    Task ClearWishlistAsync(string userId);
}
