using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AuthDtos;

namespace FreshVeggies.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var endpountGroup = app.MapGroup("/api/auth").WithTags("Auth");

            endpountGroup.MapPost("/register", async (RegisterDto model, IAuthService _authService) =>
            {
                var response = await _authService.RegisterAsync(model);

                return Results.Ok(response);
            })
            .Produces<ApiResult>()
            .WithName("Register");

            endpountGroup.MapPost("/login", async (LoginDto model, IAuthService _authService) =>
            {
                var response = await _authService.LoginAsync(model);
                return Results.Ok(response);
            })
            .Produces<ApiResult<LoggedInUser>>()
            .WithName("Login");

            return endpountGroup;
        }
    }
}
