using FreshVeggies.Shared.Dtos.AddressDtos;
using FreshVeggies.Shared.Dtos.AuthDtos;
using FreshVeggies.Shared.Dtos;
using Refit;

namespace FreshVeggies.Client.Apis
{
    [Headers("Authorization: Bearer ")]
    public interface IUserService
    {
        [Post("/api/user/changepassword")]
        Task<ApiResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        [Post("/api/user/addresses")]
        Task<ApiResult> CreateAddressAsync(AddressDto addressDto);

        [Get("/api/user/addresses")]
        Task<AddressDto[]> GetUserAddressesAsync();
    }
}
