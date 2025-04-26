using Application.Request;
using Application.Response;

namespace Application.Services.Interfaces
{
    public interface IServiceAuth
    {
        Task<int?> Register(RegistrationUserRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
