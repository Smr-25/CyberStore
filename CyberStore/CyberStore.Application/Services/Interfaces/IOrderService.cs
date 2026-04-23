using CyberStore.Core.Entities;

namespace CyberStore.Application.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
    Task<Order?> CreateOrderFromCartAsync(string userId, string customerName, string email, string address, string phone);
    Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
}

