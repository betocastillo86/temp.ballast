using Ballast.Application.Exceptions;
using Ballast.Application.Ports;
using Ballast.Application.Security;
using Ballast.Domain.Entities;
using MediatR;

namespace Ballast.Application.Users.Commands.ValidateLoginCommand;

public class ValidateLoginCommandHandler : IRequestHandler<ValidateLoginCommand, ValidateLoginCommandResult>
{
    private readonly IUserRepository _userRepository;
    
    private readonly IPasswordHasher _passwordHasher;
    
    private readonly ITokenService _tokenService;

    public ValidateLoginCommandHandler(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<ValidateLoginCommandResult> Handle(ValidateLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var passwordHash = _passwordHasher.HashPassword(request.Password, user.Salt);
        if (user.Password != passwordHash)
        {
            throw new InvalidPasswordException();
        }

        var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name);
        
        return new ValidateLoginCommandResult
        {
            UserId = user.Id,
            Token = token
        };
    }
}