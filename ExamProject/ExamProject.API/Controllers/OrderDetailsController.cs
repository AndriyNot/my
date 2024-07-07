using ExamProject.API.Models.Dtos;
using ExamProject.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            var orderDetails = await _orderDetailService.GetOrderDetailsAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail)
        {
            var createdOrderDetail = await _orderDetailService.CreateOrderDetailAsync(orderDetail);
            return CreatedAtAction(nameof(GetOrderDetail), new { id = createdOrderDetail.Id }, createdOrderDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.Id)
            {
                return BadRequest();
            }

            await _orderDetailService.UpdateOrderDetailAsync(orderDetail);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            await _orderDetailService.DeleteOrderDetailAsync(id);

            return NoContent();
        }
    }
}
