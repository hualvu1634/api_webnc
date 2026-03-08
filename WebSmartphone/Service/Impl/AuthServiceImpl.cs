using Microsoft.EntityFrameworkCore;
using WebSmartphone.Data;
using WebSmartphone.dto.request;
using WebSmartphone.dto.response;
using WebSmartphone.Models;

namespace WebSmartphone.Service.Impl
{
    public class AuthServiceImpl : AuthService
    {
        private readonly AppDbContext _context;

        public AuthServiceImpl(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null) return null;

            return new LoginResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
