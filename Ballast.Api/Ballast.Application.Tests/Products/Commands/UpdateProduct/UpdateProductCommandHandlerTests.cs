using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Products.Commands.UpdateProduct;
using Ballast.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Ballast.Application.Tests.Products.Commands.UpdateProduct;

[TestFixture]
public class UpdateProductCommandHandlerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private UpdateProductCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new UpdateProductCommandHandler(_mockProductRepository.Object);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldUpdateProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Description = "Test Product Description",
            Price = 9.99m
        };
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
        _mockProductRepository.Setup(x => x.UpdateBasicInformationAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);
        var request = new UpdateProductCommand
        {
            Id = product.Id,
            Name = "Updated Product",
            Description = "Updated Product Description",
            Price = 19.99m
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockProductRepository.Verify(x => x.UpdateBasicInformationAsync(It.IsAny<Product>()), Times.Once);
    }

    [Test]
    public void Handle_GivenANonExistentProduct_ShouldThrowNotFoundException()
    {
        // Arrange
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product)null);
        
        var request = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated Product",
            Description = "Updated Product Description",
            Price = 19.99m
        };

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockProductRepository.Verify(x => x.DeleteByIdAsync(It.IsAny<Guid>()), Times.Never);
    }

}