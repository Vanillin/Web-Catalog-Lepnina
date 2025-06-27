using Application.Mappings;
using Application.Request;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Npgsql;

namespace ApplicationUnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepositUser> _userRepositoryMock;
        private readonly Mock<IRepositReview> _reviewRepositoryMock;
        private readonly Mock<IRepositFavorites> _favoriteRepositoryMock;
        private readonly IMapper _mapper;
        private NpgsqlConnection _connection;
        private readonly Mock<ILogger<ServiceUser>> _loggerMock;
        private readonly Mock<IPasswordHasher> _passwordMock;

        private readonly IServiceUser _userService;
        private readonly User _testUser;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IRepositUser>();
            _reviewRepositoryMock = new Mock<IRepositReview>();
            _favoriteRepositoryMock = new Mock<IRepositFavorites>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _connection = new NpgsqlConnection();
            _loggerMock = new Mock<ILogger<ServiceUser>>();
            _passwordMock = new Mock<IPasswordHasher>();

            _userService = new ServiceUser(_userRepositoryMock.Object, _reviewRepositoryMock.Object, _favoriteRepositoryMock.Object, _mapper, _connection, _loggerMock.Object, _passwordMock.Object);
            _testUser = new User() { Id = 1, Name = "Test", IdPictureIcon = 1, Email = "Test@mail.ru", Role = Domain.Enums.UserRoles.Admin, PasswordHash = "1234" };
        }

        [Fact]
        public void ShouldBeAvailableToCreate()
        {
            // Assert
            _userService.Should().NotBeNull();
        }

        [Fact]
        public async Task ReadById_ValidRequest_ShouldReturnUserResponse()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.ReadById(_testUser.Id))
                .ReturnsAsync(_testUser);

            // Act
            var result = await _userService.ReadById(_testUser.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(_testUser.Id);
            result.Name.Should().Be(_testUser.Name);
            result.IdPictureIcon.Should().Be(_testUser.IdPictureIcon);
            result.Email.Should().Be(_testUser.Email);
            result.Role.Should().Be(_testUser.Role.ToString());
            result.PasswordHash.Should().Be(_testUser.PasswordHash);
            _userRepositoryMock.Verify(x => x.ReadById(_testUser.Id), Times.Once);
        }

        [Fact]
        public async Task ReadAll_ValidRequest_ShouldReturnUserResponse()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.ReadAll())
                .ReturnsAsync(new List<User>() { _testUser });

            // Act
            var result = await _userService.ReadAll();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(1);
            result.First().Id.Should().Be(_testUser.Id);
            result.First().Name.Should().Be(_testUser.Name);
            result.First().IdPictureIcon.Should().Be(_testUser.IdPictureIcon);
            result.First().Email.Should().Be(_testUser.Email);
            result.First().Role.Should().Be(_testUser.Role.ToString());
            result.First().PasswordHash.Should().Be(_testUser.PasswordHash);
            _userRepositoryMock.Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public async Task Update_ValidRequest_ShouldUpdateUser()
        {
            // Arrange
            var request = new UpdateUserRequest
            {
                Id = _testUser.Id,
                Name = _testUser.Name
            };

            _userRepositoryMock.Setup(x => x.ReadById(_testUser.Id))
                .ReturnsAsync(_testUser);
            _userRepositoryMock.Setup(x => x.Update(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            await _userService.Update(request);

            // Assert
            _userRepositoryMock.Verify(x => x.ReadById(_testUser.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.Update(It.Is<User>(c =>
                c.Id == request.Id &&
                c.Name == request.Name)), Times.Once);
        }

        //[Fact]
        //public async Task Delete_ExistingUser_ShouldDeleteUser()
        //{
        //}
    }
}
