using Microsoft.AspNetCore.Mvc;
using CyberStore.Models;
using CyberStore.Services.Interfaces;

namespace CyberStore.Controllers;

public class SomeViewController(
    IProductService productService,
    ICartService cartService,
    IWishlistService wishlistService,
    IContactService contactService)
    : Controller
{
    // Page 3 - Home Page
    public async Task<IActionResult> Page3()
    {
        var products = await productService.GetAllProductsAsync();
        return View(products);
    }

    // Page 4 - Shop / Product Details
    public async Task<IActionResult> Page4(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            var allProducts = await productService.GetAllProductsAsync();
            product = allProducts.FirstOrDefault(); // fallback
        }
        return View(product);
    }

    // Page 1 - Shopping Cart
    public async Task<IActionResult> Page1()
    {
        // Hardcoded UserId for demo since there is no Auth yet
        string userId = "demo-user-1"; 
        
        var cartItems = await cartService.GetCartItemsAsync(userId);
        return View(cartItems);
    }

    // Add to Cart Logic
    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        string userId = "demo-user-1";
        await cartService.AddToCartAsync(userId, productId, quantity);
        return RedirectToAction(nameof(Page1));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        await cartService.RemoveFromCartAsync(id);
        return RedirectToAction(nameof(Page1));
    }

    // Page 5 - Wishlist
    public async Task<IActionResult> Page5()
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
        return RedirectToAction(nameof(Page5));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromWishlist(int id)
    {
        await wishlistService.RemoveFromWishlistAsync(id);
        return RedirectToAction(nameof(Page5));
    }

    // Page 2 - Contact Us
    public IActionResult Page2()
    {
        return View(new ContactMessage());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitContact(ContactMessage model)
    {
        if (ModelState.IsValid)
        {
            await contactService.SubmitMessageAsync(model);
            TempData["SuccessMessage"] = "Your message has been sent successfully!";
            return RedirectToAction(nameof(Page2));
        }
        
        return View("Page2", model);
    }
}
