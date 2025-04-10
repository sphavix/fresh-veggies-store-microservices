using FreshVeggies.Shared.Dtos.AuthDtos;
using FreshVeggies.Shared.Dtos;
using Refit;

namespace FreshVeggies.Client.Apis
{
    public interface IAuthService
    {
        [Post("/api/auth/login")]
        Task<ApiResult<LoggedInUser>> LoginAsync(LoginDto loginDto);

        [Post("/api/auth/register")]
        Task<ApiResult> RegisterAsync(RegisterDto registerDto);
    }
}
