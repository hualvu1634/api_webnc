using WebSmartphone.dto.request;
using WebSmartphone.dto.response;

namespace WebSmartphone.Service
{
    public interface ProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse> CreateAsync(ProductRequest request);
        Task<bool> UpdateAsync(int id, ProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
