
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
                Email = user.Email,
                UserId = user.UserId,
                Role = user.Role.ToString(),
            };
        }
        
        public async Task<(bool IsSuccess, string ErrorMessage, UserResponse? Data)> RegisterAsync(UserRequest request)
        {
            // 1. Kiểm tra Email đã tồn tại chưa
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailExists) return (false, "Email này đã được sử dụng!", null);

            // 2. Kiểm tra Số điện thoại đã tồn tại chưa
            bool phoneExists = await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber);
            if (phoneExists) return (false, "Số điện thoại này đã được sử dụng!", null);

            // 3. Tạo User mới
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password, 
                PhoneNumber = request.PhoneNumber,
                Role = Role.USER // Mặc định role là USER khi đăng ký mới
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(); // Lưu để lấy được UserId

  
            var newCart = new Cart
            {
                UserId = newUser.UserId
            };
            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            var responseData = new UserResponse
            {
                UserId = newUser.UserId,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Role = newUser.Role.ToString()
            };

            return (true, string.Empty, responseData);
        }
    }
}
