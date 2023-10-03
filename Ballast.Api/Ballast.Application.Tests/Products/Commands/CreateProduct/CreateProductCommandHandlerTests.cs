using Ballast.Application.Ports;
using Ballast.Application.Products.Commands.CreateProduct;
using Ballast.Domain.Entities;
using MediatR;
using Moq;

namespace Ballast.Application.Tests.Products.Commands.CreateProduct;

[TestFixture]
public class CreateProductCommandHandlerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private CreateProductCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
    }

    [Test]
    public async Task Handle_ValidData_ShouldReturnProduct()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Product Description",
            Price = 10.99m
        };
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price
        };
        

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _productRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<Product>()), Times.Once);
    }
}