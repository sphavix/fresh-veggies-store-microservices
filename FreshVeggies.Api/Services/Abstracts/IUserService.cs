using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AddressDtos;
using FreshVeggies.Shared.Dtos.AuthDtos;

namespace FreshVeggies.Api.Services.Abstracts
{
    public interface IUserService
    {
        Task<ApiResult> ChangePassword(int userId, ChangePasswordDto changePasswordDto);
        Task<ApiResult> CreateAddress(AddressDto addressDto, int userId);
        Task<AddressDto[]> GetUserAddressesAsync(int userId);
    }
}