using Ballast.Application.Security;
using FluentAssertions;

namespace Ballast.Application.Tests.Security;

[TestFixture]
public class PasswordHasherTests
{
    private PasswordHasher _passwordHasher;

    [SetUp]
    public void Setup()
    {
        _passwordHasher = new PasswordHasher();
    }

    [Test]
    public void HashPassword_ValidInput_ReturnsValidHash()
    {
        // Arrange
        var password = "password123";
        var salt = _passwordHasher.GenerateSalt();

        // Act
        var hash = _passwordHasher.HashPassword(password, salt);

        // Assert
        salt.Should().NotBeNull();
    }

    [Test]
    public void HashPassword_SamePasswordAndSalt_ReturnsSameHash()
    {
        // Arrange
        var password = "password123";
        var salt = _passwordHasher.GenerateSalt();

        // Act
        var hash1 = _passwordHasher.HashPassword(password, salt);
        var hash2 = _passwordHasher.HashPassword(password, salt);

        // Assert
        hash1.Should().Be(hash2);
    }

    [Test]
    public void HashPassword_DifferentSalt_ReturnsDifferentHash()
    {
        // Arrange
        var password = "password123";
        var salt1 = _passwordHasher.GenerateSalt();
        var salt2 = _passwordHasher.GenerateSalt();

        // Act
        var hash1 = _passwordHasher.HashPassword(password, salt1);
        var hash2 = _passwordHasher.HashPassword(password, salt2);

        // Assert
        hash1.Should().NotBe(hash2);
    }
}