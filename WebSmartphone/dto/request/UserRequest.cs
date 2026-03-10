using System.ComponentModel.DataAnnotations;

namespace WebSmartphone.dto.request;

public class UserRequest
{
    [Required(ErrorMessage = "Họ không được để trống")]
    [StringLength(50, ErrorMessage = "Họ không vượt quá 50 ký tự")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Tên không được để trống")]
    [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
    [StringLength(50, ErrorMessage = "Email không vượt quá 50 ký tự")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    [StringLength(50, ErrorMessage = "Mật khẩu không vượt quá 50 ký tự")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Định dạng số điện thoại Việt Nam không hợp lệ")]
    public string PhoneNumber { get; set; } = null!;
}