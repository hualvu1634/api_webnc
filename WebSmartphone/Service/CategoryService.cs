using System.Threading.Tasks;
using WebSmartphone.dto.request;
using WebSmartphone.dto.response;

namespace WebSmartphone.Service
{
    public interface CategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<IEnumerable<ProductResponse>> GetByIdAsync(int id);
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<bool> UpdateAsync(int id, CategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
