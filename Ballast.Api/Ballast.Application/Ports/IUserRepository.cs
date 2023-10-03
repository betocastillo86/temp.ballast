using System.Collections.Immutable;
using Ballast.Domain.Entities;

namespace Ballast.Application.Ports;

public interface IUserRepository
{
    Task InsertAsync(User user);
    
    Task<User> GetByEmailAsync(string email);
}