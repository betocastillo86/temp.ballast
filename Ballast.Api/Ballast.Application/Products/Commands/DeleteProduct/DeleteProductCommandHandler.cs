using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using MediatR;

namespace Ballast.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id.ToString());
        }
        
        await _productRepository.DeleteByIdAsync(request.Id);
    }
}