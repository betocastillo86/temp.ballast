using AutoFixture;
using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Products.Queries.GetProductDetailById;
using Ballast.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Ballast.Application.Tests.Products.Queries.GetProductDetailById;

[TestFixture]
public class GetProductDetailByIdQueryHandlerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private GetProductDetailByIdQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new GetProductDetailByIdQueryHandler(_mockProductRepository.Object);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldReturnProductDetail()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Create<GetProductDetailByIdQuery>();
        
        var product = fixture.Build<Product>()
            .With(x => x.Id, query.Id)
            .Create();
        
        var productDetailModel = fixture.Build<ProductDetailModel>()
            .With(x => x.Id, query.Id)
            .Create();
        
        _mockProductRepository.Setup(x => x.GetByIdAsync(query.Id))
            .ReturnsAsync(product);
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<ProductDetailModel>();
        result.Id.Should().Be(query.Id);
        result.Name.Should().Be(product.Name);
        result.Description.Should().Be(product.Description);
        result.Price.Should().Be(product.Price);
        result.StockQuantity.Should().Be(product.StockQuantity);
    }

    [Test]
    public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
    {
        // Arrange
        var query = new GetProductDetailByIdQuery(Guid.NewGuid());
        
        _mockProductRepository.Setup(x => x.GetByIdAsync(query.Id))
            .ReturnsAsync((Product)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
    }
    
}