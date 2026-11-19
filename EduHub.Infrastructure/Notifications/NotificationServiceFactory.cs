using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService GetNotificationService(string channel)
    {
        return channel?.ToLowerInvariant() switch
        {
            "sms" => new SmsNotificationService(),
            _ => new EmailNotificationService()
        };
    }
}
