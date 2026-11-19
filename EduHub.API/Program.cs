using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

namespace EduHub.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Swagger / OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Clean Architecture Dependency Injection
        builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
        builder.Services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();
        builder.Services.AddTransient<INotificationService, EmailNotificationService>();
        builder.Services.AddScoped<EventService>(sp =>
            new EventService(
                sp.GetRequiredService<IEventRepository>(),
                sp.GetRequiredService<INotificationServiceFactory>()));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduHub API v1");
            c.RoutePrefix = "swagger";
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
