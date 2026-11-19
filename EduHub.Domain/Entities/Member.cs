namespace EduHub.Domain.Entities;

public class Member : Entity
{
    public string Email { get; private set; }

    private Member(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public static Member Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (!email.Contains('@'))
            throw new ArgumentException("Email must contain '@'.", nameof(email));

        return new Member(Guid.NewGuid(), email);
    }

    public static Member Create(Guid id, string email)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty.", nameof(id));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (!email.Contains('@'))
            throw new ArgumentException("Email must contain '@'.", nameof(email));

        return new Member(id, email);
    }
}
