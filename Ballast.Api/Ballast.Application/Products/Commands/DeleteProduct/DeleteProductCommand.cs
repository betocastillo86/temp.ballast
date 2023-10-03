using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using MediatR;

namespace Ballast.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}

