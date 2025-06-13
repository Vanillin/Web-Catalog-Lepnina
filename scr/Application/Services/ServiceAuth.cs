using Application.Exception;
using Application.Request;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Application.Services
{
    public class ServiceAuth : IServiceAuth
    {
        private IRepositUser _repositUser;
        private ILogger<ServiceUser> _logger;
        private IPasswordHasher _hasher;
        private IConfiguration _configuration;
        public ServiceAuth(IRepositUser repositUser, ILogger<ServiceUser> logger, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _repositUser = repositUser;
            _logger = logger;
            _hasher = passwordHasher;
            _configuration = configuration;
        }
        public async Task<ClaimsPrincipal> Register(RegistrationUserRequest request)
        {
            //The UserRoles.Admin is created only manually in the database.
            var result = await _repositUser.Create(new User()
            {
                Name = request.Name,
                IdPictureIcon = request.IdPictureIcon,
                Email = request.Email,
                PasswordHash = _hasher.HashPassword(request.Password),
                Role = UserRoles.User,
            });

            if (result == null) throw new EntityCreateException("User is not created");

            var createdUser = await _repositUser.ReadById((int)result);

            if (createdUser == null) throw new EntityCreateException("User is not created");

            _logger.LogInformation("User created with id {UserId}", result);

            var principal = GenerateClaimsPrincipal(createdUser);
            return principal;
        }
        public async Task<ClaimsPrincipal> Login(LoginRequest request)
        {
            var user = await _repositUser.ReadByEmail(request.Email);
            var passwordVerified =
                _hasher.VerifyPassword(request.Password, user?.PasswordHash);
            if (user == null || user?.PasswordHash == null || !passwordVerified)
            {
                throw new UnauthorizedAccessException();
            }
            var principal = GenerateClaimsPrincipal(user);
            return principal;
        }

        private ClaimsPrincipal GenerateClaimsPrincipal(User user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.GivenName, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString() ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            }, "HttpOnlyAuth");

            return new ClaimsPrincipal(identity);
        }
    }
}