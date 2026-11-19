using System;

namespace EduHub.Domain.Entities;

public class Registration : Entity
{
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }

    private Registration(Guid id, Guid memberId, Guid eventId, DateTime registeredAt, RegistrationStatus status)
    {
        Id = id;
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = registeredAt;
        Status = status;
    }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId cannot be empty.", nameof(memberId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("EventId cannot be empty.", nameof(eventId));

        return new Registration(Guid.NewGuid(), memberId, eventId, DateTime.UtcNow, RegistrationStatus.Pending);
    }

    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException($"Cannot confirm registration with status '{Status}'. Only Pending registrations can be confirmed.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("Registration is already cancelled and cannot be changed.");
        }

        Status = RegistrationStatus.Cancelled;
    }
}
