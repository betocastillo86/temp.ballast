using System.Data;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using Ballast.MemorySql.Core;
using Dapper;

namespace Ballast.MemorySql.Users;

//use dapper
public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InsertAsync(User user)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            "INSERT INTO Users (Id, Name, Email, Password, Salt) VALUES (@Id, @Name, @Email, @Password, @Salt)",
            user
        );
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email }
        );
    }
}