using MediatR;

namespace Ballast.Application.Products.Queries.GetProductList;

public class GetProductListQuery : IRequest<IEnumerable<ProductListModel>>
{
    public string FilterName { get; set; }
}

public class ProductListModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
}



