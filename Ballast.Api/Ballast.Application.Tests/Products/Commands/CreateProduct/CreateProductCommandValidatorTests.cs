using Ballast.Application.Products.Commands.CreateProduct;
using FluentValidation.TestHelper;

namespace Ballast.Application.Tests.Products.Commands.CreateProduct;

[TestFixture]
public class CreateProductCommandValidatorTests
{
    private CreateProductCommandValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Test]
    public void Validate_ValidModel_ShouldPass()
    {
        var command = new CreateProductCommand
        {
            Name = null,
            Description = "Test Description",
            Price = 1.99m
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public void Validate_EmptyName_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = string.Empty,
            Description = "Test Description",
            Price = 1.99m
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public void Validate_LongerName_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = new string('*', 101),
            Description = "Test Description",
            Price = 1.99m
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public void Validate_LongerDescription_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = "Test Name",
            Description = new string('*', 501),
            Price = 1.99m
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Test]
    public void Validate_PriceZero_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = "Test Name",
            Description = "Test Description",
            Price = 0
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    
}
