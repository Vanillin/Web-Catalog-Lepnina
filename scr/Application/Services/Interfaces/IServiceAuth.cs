using Application.Request;
using System.Security.Claims;

namespace Application.Services.Interfaces
{
    public interface IServiceAuth
    {
        Task<ClaimsPrincipal> Register(RegistrationUserRequest request);
        Task<ClaimsPrincipal> Login(LoginRequest request);
    }
}
