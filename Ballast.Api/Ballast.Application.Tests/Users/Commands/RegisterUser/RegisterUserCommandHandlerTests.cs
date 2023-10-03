using AutoFixture;
using Ballast.Application.Ports;
using Ballast.Application.Security;
using Ballast.Application.Users.Commands.RegisterUserCommand;
using Ballast.Domain.Entities;
using Moq;

namespace Ballast.Application.Tests.Users.Commands.RegisterUser;

[TestFixture]
public class RegisterUserCommandHandlerTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IPasswordHasher> _passwordHasherMock;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
    }

    [Test]
    public async Task Handle_ValidRequest_ReturnsUserId()
    {
        // Arrange
        var fixture = new Fixture();
        var handler = new RegisterUserCommandHandler(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object
        );
        var request = fixture.Create<RegisterUserCommand>();
        var salt = Guid.NewGuid().ToString();
        var passwordHash = Guid.NewGuid().ToString();

        _passwordHasherMock.Setup(p => p.GenerateSalt()).Returns(salt);
        _passwordHasherMock.Setup(p => p.HashPassword(request.Password, salt)).Returns(passwordHash);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<User>()), Times.Once);
        Assert.IsInstanceOf<Guid>(result);
    }
}