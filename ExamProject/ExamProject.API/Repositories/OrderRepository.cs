using ExamProject.API.Data;
using ExamProject.API.Models.Dtos;
using ExamProject.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _dbContext.Orders
             .Include(o => o.OrderDetails)
             .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                // Логика обработки отсутствия заказа, например, выброс исключения или возврат другого значения по умолчанию
                throw new InvalidOperationException($"Order with id {id} not found.");
            }

            return order;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
