using FreshVeggies.Api.Domain.Models;
using FreshVeggies.Api.Persistence;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreshVeggies.Api.Services
{
    public class AuthService(
        ApplicationDbContext context, 
        IPasswordHasher<User> passwordHasher,
        IConfiguration configuration) : IAuthService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ApiResult> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return ApiResult.Failure("Email already in use");
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Cellphone = registerDto.Cellphone
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                return ApiResult.Failure($"An error occurred while registering the user: {ex.Message}");
            }
        }

        public async Task<ApiResult<LoggedInUser>> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Username);
            if (user is null)
            {
                return ApiResult<LoggedInUser>.Failure("This user does not exist!");
            }

            var verifyUserPassword = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (verifyUserPassword != PasswordVerificationResult.Success)
            {
                return ApiResult<LoggedInUser>.Failure("Invalid password");
            }

            // Generate JWT token
            var jwtToken = GenerateJwtToken(user); // Implement JWT token generation logic

            var loggedInUser = new LoggedInUser(user.Id, user.FirstName, user.LastName, user.Email, jwtToken);

            return ApiResult<LoggedInUser>.Success(loggedInUser);
        }

        private string GenerateJwtToken(User user)
        {
            // Implement JWT token generation logic here
            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Cellphone ?? string.Empty)

                ];
            var key = _configuration.GetValue<string>("JWT:Secret");
            var expiryMinutes = _configuration.GetValue<int>("JWT:Expiration");
            var securityKey = Encoding.UTF8.GetBytes(key);
            var symmetricKey = new SymmetricSecurityKey(securityKey);

            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
