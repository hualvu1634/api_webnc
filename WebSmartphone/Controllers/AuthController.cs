
using Microsoft.AspNetCore.Mvc;
using WebSmartphone.dto.request;
using WebSmartphone.Service;

namespace WebSmartphone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response == null)
            return Unauthorized(new { message = "Email hoặc mật khẩu không đúng!" });

        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        // Nếu thất bại (trùng email, sđt...)
        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        // Nếu thành công trả về thông tin user
        return Ok(result.Data);
    }
}