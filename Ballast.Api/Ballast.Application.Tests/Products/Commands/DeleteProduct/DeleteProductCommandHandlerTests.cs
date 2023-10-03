using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Products.Commands.DeleteProduct;
using Ballast.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Ballast.Application.Tests.Products.Commands.DeleteProduct;

[TestFixture]
public class DeleteProductCommandHandlerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private DeleteProductCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new DeleteProductCommandHandler(_mockProductRepository.Object);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldDeleteProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Description = "Test Product Description",
            Price = 1.99m
        };
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);
        _mockProductRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        // Act
        var request = new DeleteProductCommand { Id = product.Id };
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockProductRepository.Verify(x => x.DeleteByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Test]
    public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
    {
        // Arrange
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product)null);

        // Act
        var request = new DeleteProductCommand { Id = Guid.NewGuid() };
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockProductRepository.Verify(x => x.DeleteByIdAsync(It.IsAny<Guid>()), Times.Never);
    }
    
}