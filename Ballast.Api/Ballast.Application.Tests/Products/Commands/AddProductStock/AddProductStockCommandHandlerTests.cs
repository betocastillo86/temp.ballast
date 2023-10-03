using AutoFixture;
using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Products.Commands.AddProductStock;
using Ballast.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;

namespace Ballast.Application.Tests.Products.Commands.AddProductStock;

[TestFixture]
public class AddProductStockCommandHandlerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private AddProductStockCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new AddProductStockCommandHandler(_productRepositoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldThrowNotFoundException_WhenProductIsNotFound()
    {
        // Arrange
        var command = new AddProductStockCommand();
        var notFoundException = new NotFoundException("Product", "1");
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddProductStockCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(notFoundException);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task Handle_ShouldCallProductRepositoryUpdateAsync_WhenCommandIsValid()
    {
        // Arrange with AutoFixture
        var fixture = new Fixture();
        var command = fixture.Create<AddProductStockCommand>();
        var product = fixture.Create<Product>();
        
        _productRepositoryMock.Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync(product);
        
        // Act

        await _handler.Handle(command, CancellationToken.None);

        // Assert

        _productRepositoryMock.Verify(x => x.UpdateStockAsync(product), Times.Once);
    }
}