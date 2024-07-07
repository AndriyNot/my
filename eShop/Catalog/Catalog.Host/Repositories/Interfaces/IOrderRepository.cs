using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderAsync(int orderId);
        Task<List<Order>> GetOrdersAsync(string userId);
        Task CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
