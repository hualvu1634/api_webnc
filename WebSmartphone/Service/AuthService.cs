using WebSmartphone.dto.request;
using WebSmartphone.dto.response;
using WebSmartphone.Models;

namespace WebSmartphone.Service
{
    public interface AuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}
