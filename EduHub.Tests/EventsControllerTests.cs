using System;
using System.Collections.Generic;
using EduHub.API.Controllers;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EduHub.Tests;

public class EventsControllerTests
{
    [Fact]
    public void GetByClub_ReturnsOkWithEvents()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var mockRepo = new Mock<IEventRepository>();
        var eventsList = new List<Event>
        {
            Event.Create(clubId, "Event 1", DateTime.UtcNow, 50),
            Event.Create(clubId, "Event 2", DateTime.UtcNow, 30)
        };
        mockRepo.Setup(r => r.GetByClubId(clubId)).Returns(eventsList);

        var mockNotifier = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotifier.Object);
        var controller = new EventsController(mockRepo.Object, service);

        // Act
        var result = controller.GetByClub(clubId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedEvents = Assert.IsAssignableFrom<IEnumerable<Event>>(okResult.Value);
        Assert.Equal(2, (returnedEvents as List<Event>)?.Count);
    }

    [Fact]
    public void GetById_WhenNotFound_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        mockRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((Event?)null);

        var mockNotifier = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotifier.Object);
        var controller = new EventsController(mockRepo.Object, service);

        // Act
        var result = controller.GetById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void CreateEvent_WhenValid_ReturnsCreatedAtAction()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var mockRepo = new Mock<IEventRepository>();
        var mockNotifier = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotifier.Object);
        var controller = new EventsController(mockRepo.Object, service);

        var request = new CreateEventRequest("Hoi thao Khoa hoc", DateTime.UtcNow.AddDays(3), 80, "email");

        // Act
        var result = controller.CreateEvent(clubId, request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdEvent = Assert.IsType<Event>(createdResult.Value);
        Assert.Equal(request.Title, createdEvent.Title);
        Assert.Equal(clubId, createdEvent.ClubId);
    }

    [Fact]
    public void CreateEvent_WhenTitleIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var mockRepo = new Mock<IEventRepository>();
        var mockNotifier = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotifier.Object);
        var controller = new EventsController(mockRepo.Object, service);

        var request = new CreateEventRequest("", DateTime.UtcNow, 50, "email");

        // Act
        var result = controller.CreateEvent(clubId, request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void Delete_WhenNotFound_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        mockRepo.Setup(r => r.Delete(It.IsAny<Guid>())).Returns(false);

        var mockNotifier = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotifier.Object);
        var controller = new EventsController(mockRepo.Object, service);

        // Act
        var result = controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void Delete_WhenFound_ReturnsNoContent()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        mockRepo.Setup(r => r.Delete(It.IsAny<Guid>())).Returns(true);

        var mockNotifier = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotifier.Object);
        var controller = new EventsController(mockRepo.Object, service);

        // Act
        var result = controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
