using MediatR;

namespace Ballast.Application.Products.Commands.AddProductStock;

public class AddProductStockCommand : IRequest
{
    public Guid Id { get; set; }
    
    public int StockQuantity { get; set; }
}

