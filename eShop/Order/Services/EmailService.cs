using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using OrderService.Models;
using OrderService.Services.Interfaces;
using Org.BouncyCastle.Crypto.Macs;

namespace OrderService.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string recipientName, string recipientEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("EShop", "fartuhandriy2003@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(recipientName, recipientEmail));
            emailMessage.Subject = subject;

            // Створення тіла листа
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message;

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("fartuhandriy2003@gmail.com", "sksz hjnp ojqp cdyw");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}