using FreshVeggies.Api.Extensions;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AddressDtos;
using FreshVeggies.Shared.Dtos.AuthDtos;
using System.Security.Claims;

namespace FreshVeggies.Api.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
           var endpointsGroup = app.MapGroup("/api/user")
                .RequireAuthorization()
                .WithTags("User");

            endpointsGroup.MapPost("/addresses", async (IUserService _userService, AddressDto model, ClaimsPrincipal principal) =>
            {
                var response = await _userService.CreateAddressAsync(model, principal.GetUserId());
                return Results.Ok(response);
            })
            .WithName("CreateAddress")
            .Produces<ApiResult<AddressDto>>(StatusCodes.Status201Created)
            .Produces<ApiResult>(StatusCodes.Status400BadRequest);

            endpointsGroup.MapGet("/addresses", async (IUserService _userService, ClaimsPrincipal principal) =>
            {
                var response = await _userService.GetUserAddressesAsync(principal.GetUserId());
                return Results.Ok(response);
            })
            .WithName("CreateAddress");

            endpointsGroup.MapPost("/changepassword", async (IUserService _userService, int userId, ChangePasswordDto model, ClaimsPrincipal principal) =>
            {
                var response = await _userService.ChangePasswordAsync(principal.GetUserId(), model);
                return Results.Ok(response);
            })
            .WithName("ChangePassword")
            .Produces<ApiResult>();

            return endpointsGroup;
        }
    }
}
