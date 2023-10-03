using AutoFixture;
using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Security;
using Ballast.Application.Users.Commands.ValidateLoginCommand;
using Ballast.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Ballast.Application.Tests.Users.Commands.ValidateLogin;

[TestFixture]
public class ValidateLoginCommandHandlerTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IPasswordHasher> _passwordHasherMock;
    private Mock<ITokenService> _tokenServiceMock;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _tokenServiceMock = new Mock<ITokenService>();
    }

    [Test]
    public async Task Handle_ValidLogin_ReturnsResult()
    {
        // Arrange
        var fixture = new Fixture();
        var handler = new ValidateLoginCommandHandler(_userRepositoryMock.Object, _passwordHasherMock.Object, _tokenServiceMock.Object);
        
        var request = fixture.Create<ValidateLoginCommand>();
        
        var passwordHash = Guid.NewGuid().ToString();
        var token = Guid.NewGuid().ToString();
        
        var user = fixture.Build<User>()
            .With(u => u.Password, passwordHash)
            .Create();

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(p => p.HashPassword(request.Password, user.Salt)).Returns(passwordHash);
        _tokenServiceMock.Setup(t => t.GenerateToken(user.Id, user.Email, user.Name)).Returns(token);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetByEmailAsync(request.Email), Times.Once);
        _passwordHasherMock.Verify(p => p.HashPassword(request.Password, user.Salt), Times.Once);
        _tokenServiceMock.Verify(t => t.GenerateToken(user.Id, user.Email, user.Name), Times.Once);

        result.Should().NotBeNull();
        result.UserId.Should().Be(user.Id);
        result.Token.Should().Be(token);
    }

    [Test]
    public void Handle_UserNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var fixture = new Fixture();
        var handler = new ValidateLoginCommandHandler(_userRepositoryMock.Object, _passwordHasherMock.Object, _tokenServiceMock.Object);
        
        var request = fixture.Create<ValidateLoginCommand>();
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email)).ReturnsAsync((User)null);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
    }

    [Test]
    public void Handle_InvalidPassword_ThrowsInvalidPasswordException()
    {
        // Arrange
        var fixture = new Fixture();
        var handler = new ValidateLoginCommandHandler(_userRepositoryMock.Object, _passwordHasherMock.Object, _tokenServiceMock.Object);
        
        var request = fixture.Create<ValidateLoginCommand>();
        var user = fixture.Create<User>();
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(p => p.HashPassword(request.Password, user.Salt)).Returns(Guid.NewGuid().ToString());

        // Act & Assert
        Assert.ThrowsAsync<InvalidPasswordException>(async () => await handler.Handle(request, CancellationToken.None));
    }
}