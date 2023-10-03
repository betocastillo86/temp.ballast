using System.Collections.Immutable;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using MediatR;

namespace Ballast.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Description = request.Description
        };
        
        await _productRepository.InsertAsync(product);
        return product.Id;
    }
}