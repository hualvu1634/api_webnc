namespace WebSmartphone.dto.response;

public class ProductResponse
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Descriptions { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int? Quantity { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; } // Lấy thêm tên danh mục cho tiện hiển thị UI
    public DateTime? ProductDate { get; set; }
}