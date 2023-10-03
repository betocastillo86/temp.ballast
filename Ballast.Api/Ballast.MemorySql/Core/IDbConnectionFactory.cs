using System.Data;

namespace Ballast.MemorySql.Core;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}