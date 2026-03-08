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
        var created = await _productService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = created.ProductId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request)
    {
        var success = await _productService.UpdateAsync(id, request);
        return success ? NoContent() : NotFound(new { message = "Không tìm thấy sản phẩm để cập nhật" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _productService.DeleteAsync(id);
        return success ? NoContent() : NotFound(new { message = "Không tìm thấy sản phẩm để xóa" });
    }
}