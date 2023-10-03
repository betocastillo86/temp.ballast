using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using MediatR;

namespace Ballast.Application.Products.Commands.AddProductStock;

public class AddProductStockCommandHandler : IRequestHandler<AddProductStockCommand>
{
    private readonly IProductRepository _productRepository;

    public AddProductStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Handle(AddProductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id.ToString());
        }
        
        product.AddStock(request.StockQuantity);
        
        await _productRepository.UpdateStockAsync(product);
    }
}