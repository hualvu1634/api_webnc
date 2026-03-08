using Microsoft.AspNetCore.Mvc;
using WebSmartphone.dto.request;
using WebSmartphone.Service;

namespace WebSmartphone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _categoryService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _categoryService.GetByIdAsync(id);
        return response != null ? Ok(response) : NotFound(new { message = "Không tìm thấy danh mục" });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryRequest request)
    {
        var created = await _categoryService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = created.CategoryId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request)
    {
        var success = await _categoryService.UpdateAsync(id, request);
        return success ? NoContent() : NotFound(new { message = "Không tìm thấy danh mục để cập nhật" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _categoryService.DeleteAsync(id);
        return success ? NoContent() : NotFound(new { message = "Không tìm thấy danh mục để xóa" });
    }
}