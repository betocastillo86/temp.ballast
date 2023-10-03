using AutoFixture;
using Ballast.Application.Users.Commands.RegisterUserCommand;
using FluentValidation.TestHelper;

namespace Ballast.Application.Tests.Users.Commands.RegisterUser;


[TestFixture]
public class RegisterUserCommandValidatorTests
{
    private RegisterUserCommandValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new RegisterUserCommandValidator();
    }

    [Test]
    public void Should_Validate_Valid()
    {
        var fixture = new Fixture();
        
        var command = fixture.Create<RegisterUserCommand>();
        
        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
}