using Microsoft.EntityFrameworkCore;
using WebSmartphone.Data;
using WebSmartphone.dto.request;
using WebSmartphone.dto.response;
using WebSmartphone.Models;
using WebSmartphone.Service;

namespace WebSmartphone.Service.Impl;

public class ProductServiceImpl : ProductService
{
    private readonly AppDbContext _context;

    public ProductServiceImpl(AppDbContext context) => _context = context;

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category)
            .Select(p => new ProductResponse
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Descriptions = p.Descriptions,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.CategoryName : null,
                ProductDate = p.ProductDate
            }).ToListAsync();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var p = await _context.Products.Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.ProductId == id);

        if (p == null) return null;

        return new ProductResponse
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Descriptions = p.Descriptions,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Quantity = p.Quantity,
            CategoryId = p.CategoryId,
            CategoryName = p.Category != null ? p.Category.CategoryName : null,
            ProductDate = p.ProductDate
        };
    }

    public async Task<ProductResponse> CreateAsync(ProductRequest request)
    {
        var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == request.CategoryId);
        if (!categoryExists)
        {
            throw new ArgumentException("Danh mục không tồn tại.");
        }

    
        var nameExists = await _context.Products.AnyAsync(p => p.ProductName == request.ProductName);
        if (nameExists)
        {
            throw new ArgumentException("Tên sản phẩm đã tồn tại.");
        }
        var product = new Product
        {
            ProductName = request.ProductName,
            Descriptions = request.Descriptions,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(product.ProductId) ?? throw new Exception("Lỗi khi tạo sản phẩm");
    }

    // ... (các phần khác giữ nguyên)

    public async Task<bool> UpdateAsync(int id, ProductRequest request)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null) return false;

        
        var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == request.CategoryId);
        if (!categoryExists)
        {
            throw new KeyNotFoundException("Danh mục không tồn tại.");
        }

   
        var nameExists = await _context.Products.AnyAsync(p => p.ProductName == request.ProductName && p.ProductId != id);
        if (nameExists)
        {
            throw new ArgumentException("Tên sản phẩm đã tồn tại ");
        }

        existing.ProductName = request.ProductName;
        existing.Descriptions = request.Descriptions;
        existing.Price = request.Price;
        existing.ImageUrl = request.ImageUrl;
        existing.Quantity = request.Quantity;
        existing.CategoryId = request.CategoryId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}