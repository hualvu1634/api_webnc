using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSmartphone.dto.request;
using WebSmartphone.Service;

namespace WebSmartphone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _productService.GetByIdAsync(id);
        return response != null ? Ok(response) : NotFound(new { message = "Không tìm thấy sản phẩm" });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest request)
    {
        try
        {
            var created = await _productService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = created.ProductId }, created);
        }
        catch (ArgumentException ex)
        {
            // Bắt lỗi ArgumentException từ Service ném ra (Trùng tên, Sai danh mục)
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Bắt các lỗi hệ thống không lường trước được
            return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request)
    {
        try
        {
            var success = await _productService.UpdateAsync(id, request);
            return success ? NoContent() : NotFound(new { message = "Không tìm thấy sản phẩm" });
        }
        catch (ArgumentException ex)
        {
            // Bắt lỗi tương tự như lúc tạo mới
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _productService.DeleteAsync(id);
        return success ? NoContent() : NotFound(new { message = "Không tìm thấy sản phẩm" });
    }
}