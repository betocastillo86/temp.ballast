using MediatR;

namespace Ballast.Application.Products.Queries.GetProductDetailById;

public class GetProductDetailByIdQuery : IRequest<ProductDetailModel>
{
    public GetProductDetailByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; set; }
}

public record ProductDetailModel
{
    public ProductDetailModel(Guid id, string name, string description, decimal price, int stockQuantity)
    {
        Id = id;
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        Description = description;
    }
    
    public Guid Id { get; init; }
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public decimal Price { get; init; }
    
    public int StockQuantity { get; init; }
}
