using System.ComponentModel.DataAnnotations;

namespace WebSmartphone.dto.request;

public class ProductRequest
{
    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên sản phẩm phải từ 2 đến 50 ký tự")]
    public string ProductName { get; set; } = null!;

    [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
    public string? Descriptions { get; set; }

    [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được là số âm")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Danh mục không được để trống")]
    public int CategoryId { get; set; }
}