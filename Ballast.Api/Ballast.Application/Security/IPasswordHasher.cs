namespace Ballast.Application.Security;


public interface IPasswordHasher
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
}