using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using MediatR;

namespace Ballast.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id.ToString());
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Description = request.Description;
        
        await _productRepository.UpdateBasicInformationAsync(product);
    }
}