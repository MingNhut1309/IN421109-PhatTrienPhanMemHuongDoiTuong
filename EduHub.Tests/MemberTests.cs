using System;
using EduHub.Domain.Entities;
using Xunit;

namespace EduHub.Tests;

public class MemberTests
{
    [Fact]
    public void Create_WithValidEmail_ReturnsMember()
    {
        var email = "student@eduhub.edu.vn";
        var member = Member.Create(email);

        Assert.NotEqual(Guid.Empty, member.Id);
        Assert.Equal(email, member.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_WithEmptyOrNullEmail_ThrowsArgumentException(string? invalidEmail)
    {
        Assert.Throws<ArgumentException>(() => Member.Create(invalidEmail!));
    }

    [Fact]
    public void Create_WithoutAtSymbol_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Member.Create("invalidemail.com"));
    }
}
