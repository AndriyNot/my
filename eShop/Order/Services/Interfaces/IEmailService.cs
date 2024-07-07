namespace OrderService.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientName, string recipientEmail, string subject, string message);
    }
}
