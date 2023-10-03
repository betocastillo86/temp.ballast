using MediatR;

namespace Ballast.Application.Users.Commands.ValidateLoginCommand;

public class ValidateLoginCommand : IRequest<ValidateLoginCommandResult>
{
    public string Email { get; init; }
    
    public string Password { get; init; }
}

public class ValidateLoginCommandResult
{
    public Guid UserId { get; init; }
    
    public string Token { get; init; }
}
