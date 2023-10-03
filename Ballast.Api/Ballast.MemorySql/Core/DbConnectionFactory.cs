using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Ballast.MemorySql.Core;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }
}