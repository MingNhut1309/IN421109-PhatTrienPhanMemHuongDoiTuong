namespace EduHub.Application.Interfaces;

public interface INotificationServiceFactory
{
    INotificationService GetNotificationService(string channel);
}
