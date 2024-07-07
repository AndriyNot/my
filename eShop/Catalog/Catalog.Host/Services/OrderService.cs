using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> GetOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                FullName = order.FullName,
                City = order.City,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    Id = od.Id,
                    OrderId = od.OrderId,
                    UserId = od.UserId,
                    CatalogItemId = od.CatalogItemId,
                    Amount = od.Amount
                }).ToList()
            };
        }

        public async Task<List<OrderDto>> GetOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersAsync(userId);
            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                FullName = order.FullName,
                City = order.City,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails?.Select(od => new OrderDetailDto
                {
                    Id = od.Id,
                    OrderId = od.OrderId,
                    UserId = od.UserId,
                    CatalogItemId = od.CatalogItemId, // Оновлено на CatalogItemId
                    Amount = od.Amount
                }).ToList() ?? new List<OrderDetailDto>()
            }).ToList();
        }

        public async Task<int> CreateOrderAsync(OrderDto orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                FullName = orderDto.FullName,
                City = orderDto.City,
                Address = orderDto.Address,
                PhoneNumber = orderDto.PhoneNumber,
                Email = orderDto.Email,
                OrderDate = orderDto.OrderDate,
                OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
                {
                    UserId = od.UserId,
                    CatalogItemId = od.CatalogItemId,
                    Amount = od.Amount
                }).ToList()
            };

            await _orderRepository.CreateOrderAsync(order);
            return order.Id;
        }

        public async Task<bool> UpdateOrderAsync(OrderDto orderDto)
        {
            var order = await _orderRepository.GetOrderAsync(orderDto.Id);
            if (order == null)
            {
                return false;
            }

            order.UserId = orderDto.UserId;
            order.FullName = orderDto.FullName;
            order.City = orderDto.City;
            order.Address = orderDto.Address;
            order.PhoneNumber = orderDto.PhoneNumber;
            order.Email = orderDto.Email;
            order.OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
            {
                Id = od.Id,
                OrderId = od.OrderId,
                UserId = od.UserId,
                CatalogItemId = od.CatalogItemId,
                Amount = od.Amount
            }).ToList();

            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
