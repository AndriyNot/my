using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderService.Models;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly SmtpSettings _smtpSettings;

        public OrderController(IEmailService emailService, IOptions<SmtpSettings> smtpSettings)
        {
            _emailService = emailService;
            _smtpSettings = smtpSettings.Value;
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            // Ваша логіка оформлення замовлення

            // Виклик сервісу відправки електронної пошти з налаштуваннями
            await _emailService.SendEmailAsync(request.RecipientName, request.RecipientEmail, request.Subject, request.Message);

            return Ok("Order placed and email sent");
        }
    }
}