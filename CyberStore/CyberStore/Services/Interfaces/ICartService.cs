using CyberStore.Models;

namespace CyberStore.Services.Interfaces;

public interface ICartService
{
    Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);
    Task AddToCartAsync(string userId, int productId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task ClearCartAsync(string userId);
}
