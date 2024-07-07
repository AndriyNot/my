using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetOrderAsync(int orderId);
        Task<List<OrderDto>> GetOrdersAsync(string userId);
        Task<int> CreateOrderAsync(OrderDto orderDto);
        Task<bool> UpdateOrderAsync(OrderDto orderDto);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
