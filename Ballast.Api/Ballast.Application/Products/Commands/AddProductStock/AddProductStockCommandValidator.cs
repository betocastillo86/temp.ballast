using FluentValidation;

namespace Ballast.Application.Products.Commands.AddProductStock;

public class AddProductStockCommandValidator : AbstractValidator<AddProductStockCommand>
{
    public AddProductStockCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.StockQuantity).GreaterThan(0);
    }
}