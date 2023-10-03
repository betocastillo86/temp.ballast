namespace Ballast.Application.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid user or password.") { }
}