namespace Ballast.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string itemType, string itemId) : base($"Entity \"{itemType}\" ({itemId}) was not found.")
    {
        
    }
}