namespace WebSmartphone.dto.request;

public class ProductRequest
{
    public string ProductName { get; set; } = null!;
    public string? Descriptions { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
}