using FluentValidation;

namespace Ballast.Application.Products.Queries.GetProductList;

public class GetProductListQueryValidator : AbstractValidator<GetProductListQuery>
{
    public GetProductListQueryValidator()
    {
        RuleFor(x => x.FilterName)
            .MinimumLength(2)
            .When(c => !string.IsNullOrEmpty(c.FilterName));
    }
}