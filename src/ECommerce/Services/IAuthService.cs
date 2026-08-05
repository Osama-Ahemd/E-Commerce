using ECommerce.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto model);
        Task<string?> LoginAsync(UserLoginDto model);
    }
}
