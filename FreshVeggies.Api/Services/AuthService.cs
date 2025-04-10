using FreshVeggies.Api.Domain.Models;
using FreshVeggies.Api.Persistence;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreshVeggies.Api.Services
{
    public class AuthService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher) : IAuthService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

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
            var jwtToken = "JWT token here"; // Implement JWT token generation logic

            var loggedInUser = new LoggedInUser(user.Id, user.FirstName, user.LastName, user.Email, jwtToken);

            return ApiResult<LoggedInUser>.Success(loggedInUser);
        }
    }
}
