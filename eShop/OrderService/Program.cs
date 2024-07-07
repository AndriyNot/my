using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Services;
using OrderService.Services.Interfaces;
using OrderService.Models; // Додайте цей імпорт

var builder = WebApplication.CreateBuilder(args);

// Додавання сервісу для конфігурації SmtpSettings
builder.Configuration.Bind("SmtpSettings", new SmtpSettings());
builder.Services.AddSingleton(builder.Configuration.Get<SmtpSettings>());

// Додавання сервісів для відправки електронної пошти
builder.Services.AddTransient<IEmailService, EmailService>();

// Додавання контролера замовлень
builder.Services.AddControllers();

// Додавання Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Додавання маршруту контролера
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Замість app.MapPost, використовуємо MapControllers()

app.Run();