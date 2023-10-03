namespace Ballast.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int StockQuantity { get; private set; }
    
    public void AddStock(int quantity)
    {
        if(StockQuantity + quantity < 0)
            throw new Exception("Not enough stock");
        
        StockQuantity += quantity;
    }
}


