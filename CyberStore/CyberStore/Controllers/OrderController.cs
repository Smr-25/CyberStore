using Microsoft.AspNetCore.Mvc;
using CyberStore.Application.Services.Interfaces;

namespace CyberStore.Controllers;

public class OrderController(IOrderService orderService) : Controller
{
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string customerName, string email, string address, string phone)
    {
        if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(email) || 
            string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
        {
            ModelState.AddModelError("", "Please fill in all fields");
            return View();
        }

        string userId = "demo-user-1";
        var order = await orderService.CreateOrderFromCartAsync(userId, customerName, email, address, phone);

        if (order == null)
        {
            TempData["ErrorMessage"] = "Your cart is empty. Add items before creating an order.";
            return RedirectToAction("Cart", "Store");
        }

        TempData["SuccessMessage"] = $"Order #{order.Id} created successfully!";
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    public async Task<IActionResult> History()
    {
        string userId = "demo-user-1";
        var orders = await orderService.GetUserOrdersAsync(userId);
        return View(orders);
    }

    public async Task<IActionResult> AllOrders()
    {
        var orders = await orderService.GetAllOrdersAsync();
        return View(orders);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int orderId, int status)
    {
        var result = await orderService.UpdateOrderStatusAsync(orderId, (CyberStore.Core.Entities.OrderStatus)status);
        if (result)
        {
            TempData["SuccessMessage"] = "Order status updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update order status.";
        }
        return RedirectToAction(nameof(Details), new { id = orderId });
    }
}

