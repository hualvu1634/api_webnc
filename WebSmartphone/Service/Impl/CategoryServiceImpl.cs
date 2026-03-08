using Microsoft.EntityFrameworkCore;
using WebSmartphone.Data;
using WebSmartphone.dto.request;
using WebSmartphone.dto.response;
using WebSmartphone.Models;
using WebSmartphone.Service;

namespace WebSmartphone.Service.Impl;

public class CategoryServiceImpl : CategoryService
{
    private readonly AppDbContext _context;

    public CategoryServiceImpl(AppDbContext context) => _context = context;

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryResponse
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CategoryDate = c.CategoryDate
            }).ToListAsync();
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return null;

        return new CategoryResponse
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CategoryDate = category.CategoryDate
        };
    }

    public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
    {
        var category = new Category
        {
            CategoryName = request.CategoryName
            // CategoryDate sẽ tự động lấy GETDATE() nhờ config trong DbContext
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new CategoryResponse
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CategoryDate = category.CategoryDate
        };
    }

    public async Task<bool> UpdateAsync(int id, CategoryRequest request)
    {
        var existing = await _context.Categories.FindAsync(id);
        if (existing == null) return false;

        existing.CategoryName = request.CategoryName;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}