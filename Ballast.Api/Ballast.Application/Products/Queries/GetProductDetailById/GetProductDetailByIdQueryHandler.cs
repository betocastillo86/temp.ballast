using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using MediatR;

namespace Ballast.Application.Products.Queries.GetProductDetailById;

public class GetProductDetailByIdQueryHandler : IRequestHandler<GetProductDetailByIdQuery, ProductDetailModel>
{
    private readonly IProductRepository _productRepository;

    public GetProductDetailByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDetailModel> Handle(GetProductDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id.ToString());
        }
        
        return new ProductDetailModel(product.Id, product.Name, product.Description, product.Price, product.StockQuantity);
    }
}