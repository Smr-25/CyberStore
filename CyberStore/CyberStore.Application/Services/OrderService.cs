using CyberStore.Core.Entities;
using CyberStore.Data.Repositories.Interfaces;
using CyberStore.Application.Services.Interfaces;

namespace CyberStore.Application.Services;

public class OrderService(IUnitOfWork unitOfWork, ICartService cartService) : IOrderService
{
    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        var orders = await unitOfWork.Orders.FindAsync(o => o.Id == id);
        var order = orders.FirstOrDefault();
        return order;
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
    {
        return await unitOfWork.Orders.FindAsync(o => o.UserId == userId);
    }

    public async Task<Order?> CreateOrderFromCartAsync(string userId, string customerName, string email, string address, string phone)
    {
        var cartItems = await cartService.GetCartItemsAsync(userId);
        if (!cartItems.Any())
            return null;

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            CustomerName = customerName,
            CustomerEmail = email,
            ShippingAddress = address,
            PhoneNumber = phone
        };

        decimal totalAmount = 0;

        foreach (var cartItem in cartItems)
        {
            var orderItem = new OrderItem
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product?.Price ?? 0,
                CreatedAt = DateTime.UtcNow
            };
            order.Items.Add(orderItem);
            totalAmount += orderItem.TotalPrice;
        }

        order.TotalAmount = totalAmount;
        await unitOfWork.Orders.AddAsync(order);
        await unitOfWork.CompleteAsync();

        // Clear the cart after order creation
        foreach (var cartItem in cartItems)
        {
            await cartService.RemoveFromCartAsync(cartItem.Id);
        }

        return order;
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        var orders = await unitOfWork.Orders.FindAsync(o => o.Id == orderId);
        var order = orders.FirstOrDefault();
        
        if (order == null)
            return false;

        order.Status = status;
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await unitOfWork.Orders.GetAllAsync();
    }
}

