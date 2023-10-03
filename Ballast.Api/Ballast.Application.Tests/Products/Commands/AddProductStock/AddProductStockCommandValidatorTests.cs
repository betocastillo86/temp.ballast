using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Products.Commands.AddProductStock;
using Ballast.Domain.Entities;
using FluentAssertions;

namespace Ballast.Application.Tests.Products.Commands.AddProductStock;

[TestFixture]
public class AddProductStockCommandValidatorTests
{
    
    [Test]
    public void Validate_ValidModel_ShouldPass()
    {
        // Arrange
        var command = new AddProductStockCommand
        {
            Id = Guid.NewGuid(),
            StockQuantity = 1
        };
        var validator = new AddProductStockCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    public void Validate_IdIsEmpty_ShouldFail()
    {
        // Arrange
        var command = new AddProductStockCommand
        {
            Id = Guid.Empty,
            StockQuantity = 1
        };
        var validator = new AddProductStockCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == nameof(command.Id));
    }
    
    [Test]
    public void Validate_StockQuantityLessThanOne_ShouldFail()
    {
        // Arrange
        var command = new AddProductStockCommand
        {
            Id = Guid.NewGuid(),
            StockQuantity = 0
        };
        var validator = new AddProductStockCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == nameof(command.StockQuantity));
    }
}