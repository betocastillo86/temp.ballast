namespace Ballast.Application.Security;

public interface ITokenService
{
    string GenerateToken(Guid userId, string email, string name);
}