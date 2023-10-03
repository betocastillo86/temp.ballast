using FluentValidation;

namespace Ballast.Application.Products.Queries.GetProductDetailById;

public class GetProductDetailByIdQueryValidator : AbstractValidator<GetProductDetailByIdQuery>
{
    public GetProductDetailByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}