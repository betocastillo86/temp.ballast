using Ballast.Application.Ports;
using Ballast.MemorySql.Core;
using Ballast.MemorySql.Products;
using Ballast.MemorySql.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Ballast.MemorySql;

public static class SqlServiceRegister
{
    public static IServiceCollection AddSqlRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        return services;
    }
}