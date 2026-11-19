using System;

namespace EduHub.Domain.Entities;

public class Event : Entity
{
    public Guid ClubId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    private Event(Guid id, Guid clubId, string title, DateTime date, int capacity)
    {
        Id = id;
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
    }

    public static Event Create(Guid clubId, string title, DateTime date, int capacity)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId cannot be empty.", nameof(clubId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than 0.", nameof(capacity));

        return new Event(Guid.NewGuid(), clubId, title, date, capacity);
    }
}
