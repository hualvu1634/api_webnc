using System.ComponentModel.DataAnnotations;

namespace WebSmartphone.dto.request;

public class CategoryRequest
{
    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên danh mục phải từ 2 đến 255 ký tự")]
    public string CategoryName { get; set; } = null!;
}