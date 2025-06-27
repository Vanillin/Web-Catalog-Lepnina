using Application.Mappings;
using Application.Request;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Npgsql;

namespace ApplicationUnitTests.Services
{
    public class SectionServiceTests
    {
        private readonly Mock<IRepositSection> _sectionRepositoryMock;
        private readonly Mock<IRepositProduct> _productRepositoryMock;
        private readonly IMapper _mapper;
        private NpgsqlConnection _connection;
        private readonly Mock<ILogger<ServiceSection>> _loggerMock;

        private readonly IServiceSection _sectionService;
        private readonly Section _testSection;

        public SectionServiceTests()
        {
            _sectionRepositoryMock = new Mock<IRepositSection>();
            _productRepositoryMock = new Mock<IRepositProduct>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _connection = new NpgsqlConnection();
            _loggerMock = new Mock<ILogger<ServiceSection>>();

            _sectionService = new ServiceSection(_sectionRepositoryMock.Object, _productRepositoryMock.Object, _mapper, _connection, _loggerMock.Object);
            _testSection = new Section() { Id = 1, Name = "Test" };
        }

        [Fact]
        public void ShouldBeAvailableToCreate()
        {
            // Assert
            _sectionService.Should().NotBeNull();
        }

        [Fact]
        public async Task ReadById_ExistingSection_ShouldReturnSectionResponse()
        {
            // Arrange
            _sectionRepositoryMock.Setup(x => x.ReadById(_testSection.Id))
                .ReturnsAsync(_testSection);

            // Act
            var result = await _sectionService.ReadById(_testSection.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(_testSection.Id);
            result.Name.Should().Be(_testSection.Name);
            _sectionRepositoryMock.Verify(x => x.ReadById(_testSection.Id), Times.Once);
        }

        [Fact]
        public async Task ReadAll_ExistingSection_ShouldReturnSectionResponse()
        {
            // Arrange
            _sectionRepositoryMock.Setup(x => x.ReadAll())
                .ReturnsAsync(new List<Section>() { _testSection });

            // Act
            var result = await _sectionService.ReadAll();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(1);
            result.First().Id.Should().Be(_testSection.Id);
            result.First().Name.Should().Be(_testSection.Name);
            _sectionRepositoryMock.Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public async Task Create_ValidRequest_ShouldCreateSection()
        {
            // Arrange
            var request = new CreateSectionRequest
            {
                Name = _testSection.Name,
            };
            _sectionRepositoryMock.Setup(x => x.Create(It.IsAny<Section>()))
                .ReturnsAsync(_testSection.Id);

            // Act
            var result = await _sectionService.Create(request);

            // Assert
            result.Should().Be(_testSection.Id);
            _sectionRepositoryMock.Verify(x => x.Create(It.Is<Section>(c =>
                c.Name == request.Name)), Times.Once);
        }

        [Fact]
        public async Task Update_ExistingSection_ShouldUpdateSection()
        {
            // Arrange
            var request = new UpdateSectionRequest
            {
                Id = _testSection.Id,
                Name = _testSection.Name
            };

            _sectionRepositoryMock.Setup(x => x.ReadById(_testSection.Id))
                .ReturnsAsync(_testSection);
            _sectionRepositoryMock.Setup(x => x.Update(It.IsAny<Section>()))
                .ReturnsAsync(true);

            // Act
            await _sectionService.Update(request);

            // Assert
            _sectionRepositoryMock.Verify(x => x.ReadById(_testSection.Id), Times.Once);
            _sectionRepositoryMock.Verify(x => x.Update(It.Is<Section>(c =>
                c.Id == request.Id &&
                c.Name == request.Name)), Times.Once);
        }

        //[Fact]
        //public async Task Delete_ExistingSection_ShouldDeleteSection()
        //{
        //    // Arrange
        //    _sectionRepositoryMock.Setup(x => x.Delete(_testSection.Id))
        //        .ReturnsAsync(true);
        //    _productRepositoryMock.Setup(x => x.ReadAll())
        //        .ReturnsAsync(new List<Product>());

        //    // Act
        //    await _sectionService.Delete(_testSection.Id);

        //    // Assert
        //    _sectionRepositoryMock.Verify(x => x.Delete(_testSection.Id), Times.Once);
        //}
    }
}