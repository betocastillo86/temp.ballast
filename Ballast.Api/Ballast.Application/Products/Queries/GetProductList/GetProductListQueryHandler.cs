using Ballast.Application.Ports;
using MediatR;

namespace Ballast.Application.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductListModel>>
{
    private readonly IProductRepository _productRepository;

    public GetProductListQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductListModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.FilterProducts(name: request.FilterName);
        return products.Select(p => new ProductListModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            StockQuantity = p.StockQuantity
        });
    }
}