using System.Collections.Immutable;
using Ballast.Application.Ports;
using Ballast.Domain.Entities;
using Ballast.MemorySql.Core;
using Dapper;

namespace Ballast.MemorySql.Products;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ProductRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InsertAsync(Product product)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            "INSERT INTO Products (Id, Name, Description, Price, StockQuantity) VALUES (@Id, @Name, @Description, @Price, @StockQuantity)",
            product
        );
    }
    
    public async Task UpdateBasicInformationAsync(Product product)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id",
            product
        );
    }
    
    public async Task<Product> GetByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Product>(
            "SELECT * FROM Products WHERE Id = @Id",
            new { Id = id }
        );
    }
    
    public async Task<IImmutableList<Product>> FilterProducts(string name = null)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var sql = "SELECT * FROM Products";
        if (!string.IsNullOrWhiteSpace(name))
        {
            sql += " WHERE Name LIKE @Name";
        }
        return (await connection.QueryAsync<Product>(sql, new { Name = $"%{name}%" })).ToImmutableList();
    }
    
    public async Task DeleteByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            "DELETE FROM Products WHERE Id = @Id",
            new { Id = id }
        );
    }
    
    public async Task UpdateStockAsync(Product product)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            "UPDATE Products SET StockQuantity = @StockQuantity WHERE Id = @Id",
            product
        );
    }
}