namespace Ballast.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public string Email { get; init; }
    
    public string Password { get; init; }
    
    public string Salt { get; init; }
}
