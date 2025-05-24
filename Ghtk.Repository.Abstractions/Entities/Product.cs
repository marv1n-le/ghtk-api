namespace Ghtk.Repository.Abstractions.Entities;

public class Product
{
    public string Name { get; set; }
    
    public double Weight { get; set; }
    
    public long Quantity { get; set; }
    
    public long ProductCode { get; set; }
}