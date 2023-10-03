using System.Collections.Immutable;
using Ballast.Domain.Entities;

namespace Ballast.Application.Ports;

public interface IProductRepository
{
    Task InsertAsync(Product product);
    
    Task UpdateBasicInformationAsync(Product product);
    
    Task<Product> GetByIdAsync(Guid id);
    
    Task<IImmutableList<Product>> FilterProducts(string name = null);
    
    Task DeleteByIdAsync(Guid id);
    
    Task UpdateStockAsync(Product product);
}