using ExamProject.API.Data;
using ExamProject.API.Models.Dtos;
using ExamProject.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderDetailRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync()
        {
            return await _dbContext.OrderDetails.Include(od => od.Product).ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            var orderDetail = await _dbContext.OrderDetails
            .Include(od => od.Product)
            .FirstOrDefaultAsync(od => od.Id == id);

            if (orderDetail == null)
            {
                // Логика обработки отсутствия заказа, например, выброс исключения или возврат другого значения по умолчанию
                throw new InvalidOperationException($"Order detail with id {id} not found.");
            }

            return orderDetail;
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Add(orderDetail);
            await _dbContext.SaveChangesAsync();
            return orderDetail;
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _dbContext.Entry(orderDetail).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _dbContext.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _dbContext.OrderDetails.Remove(orderDetail);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
