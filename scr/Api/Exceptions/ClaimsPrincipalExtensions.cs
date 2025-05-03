using System.Security.Claims;

namespace Api.Exceptions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var id) ? id : null;
        }
    }
}
