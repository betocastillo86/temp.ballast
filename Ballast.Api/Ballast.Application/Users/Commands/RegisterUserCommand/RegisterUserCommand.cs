using MediatR;

namespace Ballast.Application.Users.Commands.RegisterUserCommand;

public class RegisterUserCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public string Email { get; init; }
    
    public string Password { get; init; }
}

