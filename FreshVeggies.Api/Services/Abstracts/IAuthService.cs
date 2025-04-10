using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AuthDtos;

namespace FreshVeggies.Api.Services.Abstracts
{
    public interface IAuthService
    {
        Task<ApiResult<LoggedInUser>> LoginAsync(LoginDto loginDto);
        Task<ApiResult> RegisterAsync(RegisterDto registerDto);
    }
}