using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost("get")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrder([FromBody] int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("getAll")]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders([FromBody] string userId)
        {
            var orders = await _orderService.GetOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var orderId = await _orderService.CreateOrderAsync(orderDto);
            return Ok(orderId);
        }

        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto)
        {
            var result = await _orderService.UpdateOrderAsync(orderDto);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrder([FromBody] int orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
