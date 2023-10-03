using FluentValidation;

namespace Ballast.Application.Users.Commands.ValidateLoginCommand;

public class ValidateLoginCommandValidator : AbstractValidator<ValidateLoginCommand>
{
    public ValidateLoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .NotEmpty();
    }
}