using System.Security.Claims;

namespace FreshVeggies.Api.Extensions
{
    public static class ApiExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
