using ECommerce.Configuration;
using ECommerce.Data.Models;
using ECommerce.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Services
{
    public class AuthService(UserManager<User> userManager, JwtOptions jwtOptions) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly JwtOptions _jwtOptions = jwtOptions;

        public async Task<IdentityResult> RegisterAsync(RegisterDto model)
        {
            var user = new User { UserName = model.UserName, Email = model.Email, FullName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<string?> LoginAsync(UserLoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return null;
            if (!await _userManager.CheckPasswordAsync(user, model.Password)) return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwtOptions.SigningKey ?? string.Empty);

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.Email, user.Email!),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Lifetime),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
