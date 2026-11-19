using System;
using EduHub.Domain.Entities;
using Xunit;

namespace EduHub.Tests;

public class RegistrationTests
{
    [Fact]
    public void Create_WithValidParameters_ReturnsPendingRegistration()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        // Act
        var registration = Registration.Create(memberId, eventId);

        // Assert
        Assert.NotEqual(Guid.Empty, registration.Id);
        Assert.Equal(memberId, registration.MemberId);
        Assert.Equal(eventId, registration.EventId);
        Assert.Equal(RegistrationStatus.Pending, registration.Status);
        Assert.True(registration.RegisteredAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Create_WithEmptyMemberIdOrEventId_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, Guid.NewGuid()));
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.NewGuid(), Guid.Empty));
    }

    [Fact]
    public void Confirm_WhenRegistrationIsPending_ChangesStatusToConfirmed()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());

        // Act
        r.Confirm();

        // Assert
        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Cancel_WhenRegistrationIsPending_ChangesStatusToCancelled()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());

        // Act
        r.Cancel();

        // Assert
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Cancel_WhenRegistrationIsConfirmed_ChangesStatusToCancelled()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        r.Confirm();

        // Act
        r.Cancel();

        // Assert
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Confirm_WhenRegistrationIsCancelled_ThrowsInvalidOperationExceptionAndStatusRemainsCancelled()
    {
        // Required test case by exam:
        // 1 var r = Registration.Create(memberId, eventId);
        // 2 r.Cancel();
        // 3 r.Confirm();

        // Arrange
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var r = Registration.Create(memberId, eventId);
        r.Cancel();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Cancel_WhenRegistrationIsAlreadyCancelled_ThrowsInvalidOperationException()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        r.Cancel();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => r.Cancel());
    }
}
