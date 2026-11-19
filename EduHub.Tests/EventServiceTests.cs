using System;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Moq;
using Xunit;

namespace EduHub.Tests;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_WhenValid_CallsAddAndSendExactlyOnce()
    {
        // Arrange
        var mockRepository = new Mock<IEventRepository>();
        var mockNotificationService = new Mock<INotificationService>();

        var eventService = new EventService(mockRepository.Object, mockNotificationService.Object);

        var clubId = Guid.NewGuid();
        var title = "Lap trinh huong doi tuong voi C#";
        var date = DateTime.UtcNow.AddDays(7);
        var capacity = 100;
        var channel = "email";

        // Act
        var createdEvent = eventService.CreateEvent(clubId, title, date, capacity, channel);

        // Assert
        Assert.NotNull(createdEvent);
        Assert.Equal(title, createdEvent.Title);
        Assert.Equal(clubId, createdEvent.ClubId);
        Assert.Equal(capacity, createdEvent.Capacity);

        // Verify that Add and Send were each called exactly once
        mockRepository.Verify(repo => repo.Add(It.Is<Event>(e => e.Title == title && e.ClubId == clubId)), Times.Once);
        mockNotificationService.Verify(notifier => notifier.Send(It.Is<string>(msg => msg.Contains(title))), Times.Once);
    }

    [Fact]
    public void CreateEvent_WhenTitleIsEmpty_ThrowsArgumentExceptionAndNeverCallsAddOrSend()
    {
        // Arrange
        var mockRepository = new Mock<IEventRepository>();
        var mockNotificationService = new Mock<INotificationService>();

        var eventService = new EventService(mockRepository.Object, mockNotificationService.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => eventService.CreateEvent(Guid.NewGuid(), "", DateTime.UtcNow, 50, "email"));

        mockRepository.Verify(repo => repo.Add(It.IsAny<Event>()), Times.Never);
        mockNotificationService.Verify(notifier => notifier.Send(It.IsAny<string>()), Times.Never);
    }
}
