using Application.Exception;
using Application.Request;
using Application.Response;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<int?> Register(RegistrationUserRequest request)
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

            _logger.LogInformation("User created with id {UserId}", result);
            return result;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _repositUser.ReadByEmail(request.Email);
            var passwordVerified =
                _hasher.VerifyPassword(request.Password, user?.PasswordHash);
            if (user == null || user?.PasswordHash == null || !passwordVerified)
            {
                throw new UnauthorizedAccessException();
            }
            var token = GenerateJwtToken(user);

            return new LoginResponse() { Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSecret = _configuration["JwtSettings:Secret"] ?? throw new ArgumentNullException("JwtSettings:Secret");
            var jwtIssuer = _configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("JwtSettings:Issuer");
            var jwtAudience = _configuration["JwtSettings:Audience"] ??
                              throw new ArgumentNullException("JwtSettings:Audience");
            var jwtExpirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"] ?? "60");

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                 new Claim(ClaimTypes.GivenName, user.Name ?? string.Empty),
                 new Claim(ClaimTypes.Role, user.Role.ToString() ?? string.Empty),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
             }),
                Expires = DateTime.UtcNow.AddMinutes(jwtExpirationMinutes),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}