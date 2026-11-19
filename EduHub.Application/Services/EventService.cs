using System;
using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;

namespace EduHub.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly INotificationServiceFactory? _notificationServiceFactory;
    private readonly INotificationService? _notificationService;

    public EventService(IEventRepository eventRepository, INotificationServiceFactory notificationServiceFactory)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _notificationServiceFactory = notificationServiceFactory ?? throw new ArgumentNullException(nameof(notificationServiceFactory));
    }

    public EventService(IEventRepository eventRepository, INotificationService notificationService)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel = "email")
    {
        var @event = Event.Create(clubId, title, date, capacity);
        _eventRepository.Add(@event);

        var notifier = _notificationService ?? _notificationServiceFactory?.GetNotificationService(channel);
        notifier?.Send($"Sự kiện mới: {@event.Title}");

        return @event;
    }
}
