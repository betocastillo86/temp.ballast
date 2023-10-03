using MediatR;

namespace Ballast.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
}