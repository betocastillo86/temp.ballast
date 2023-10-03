using Ballast.Application.Users.Commands.ValidateLoginCommand;
using FluentValidation.TestHelper;

namespace Ballast.Application.Tests.Users.Commands.ValidateLogin;

[TestFixture]
public class ValidateLoginCommandValidatorTests
{
    private ValidateLoginCommandValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new ValidateLoginCommandValidator();
    }

    [Test]
    public void Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var validCommand = new ValidateLoginCommand
        {
            Email = "test@example.com",
            Password = "validpassword"
        };

        // Act & Assert
        var result = _validator.TestValidate(validCommand);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Validate_InvalidEmail_ShouldFail()
    {
        // Arrange
        var invalidEmailCommand = new ValidateLoginCommand
        {
            Email = "invalidemail",
            Password = "validpassword"
        };

        // Act & Assert
        var result = _validator.TestValidate(invalidEmailCommand);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Test]
    public void Validate_EmptyEmail_ShouldFail()
    {
        // Arrange
        var emptyEmailCommand = new ValidateLoginCommand
        {
            Email = "",
            Password = "validpassword"
        };

        // Act & Assert
        var result = _validator.TestValidate(emptyEmailCommand);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Test]
    public void Validate_InvalidPasswordLength_ShouldFail()
    {
        // Arrange
        var invalidPasswordCommand = new ValidateLoginCommand
        {
            Email = "test@example.com",
            Password = "short"
        };

        // Act & Assert
        var result = _validator.TestValidate(invalidPasswordCommand);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Test]
    public void Validate_EmptyPassword_ShouldFail()
    {
        // Arrange
        var emptyPasswordCommand = new ValidateLoginCommand
        {
            Email = "test@example.com",
            Password = ""
        };

        // Act & Assert
        var result = _validator.TestValidate(emptyPasswordCommand);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}