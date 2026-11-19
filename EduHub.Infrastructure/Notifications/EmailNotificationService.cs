using System;
using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

public class EmailNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }
}
