using Microsoft.AspNetCore.Mvc;
using CyberStore.Application.Services.Interfaces;

namespace CyberStore.Controllers;

public class StoreController(
    IProductService productService,
    ICartService cartService,
    IWishlistService wishlistService)
    : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(product);
    }

    public async Task<IActionResult> Cart()
    {
        string userId = "demo-user-1"; 
        var cartItems = await cartService.GetCartItemsAsync(userId);
        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        string userId = "demo-user-1";
        await cartService.AddToCartAsync(userId, productId, quantity);
        return RedirectToAction(nameof(Cart));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        await cartService.RemoveFromCartAsync(id);
        return RedirectToAction(nameof(Cart));
    }

    public async Task<IActionResult> Wishlist()
    {
        string userId = "demo-user-1";
        var wishlistItems = await wishlistService.GetWishlistItemsAsync(userId);
        return View(wishlistItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(int productId)
    {
        string userId = "demo-user-1";
        await wishlistService.AddToWishlistAsync(userId, productId);
        return RedirectToAction(nameof(Wishlist));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromWishlist(int id)
    {
        await wishlistService.RemoveFromWishlistAsync(id);
        return RedirectToAction(nameof(Wishlist));
    }
}

