using Api.Controllers;
using Application.Dto;
using Application.Mappings;
using Application.Request;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiUnitTests.Controllers
{
    public class SectionControllerTests
    {
        private readonly Mock<IServiceSection> _sectionServiceMock;
        private readonly IMapper _mapper;

        private readonly SectionController _controller;
        private readonly Section _testSection;

        public SectionControllerTests()
        {
            _sectionServiceMock = new Mock<IServiceSection>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _controller = new SectionController(_sectionServiceMock.Object);

            _testSection = new Section
            {
                Id = 1,
                Name = "Test"
            };
        }

        [Fact]
        public async Task Create_ValidRequest_ShouldReturnOkObjectResult()
        {
            // Arrange
            var request = new CreateSectionRequest
            {
                Name = _testSection.Name,
            };

            _sectionServiceMock.Setup(x => x.Create(request))
                .ReturnsAsync(_testSection.Id);

            // Act
            var result = await _controller.Add(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().NotBeNull();

            _sectionServiceMock.Verify(x => x.Create(request), Times.Once);
        }

        [Fact]
        public async Task GetAll_ValidRequest_ShouldReturnOkObjectResult()
        {
            // Arrange
            var comments = new List<SectionDto>
            {
                _mapper.Map<SectionDto>(_testSection)
            };

            _sectionServiceMock.Setup(x => x.ReadAll())
                .ReturnsAsync(comments);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(comments);

            _sectionServiceMock.Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public async Task Update_ValidRequest_ShouldReturnOkObjectResult()
        {
            // Arrange
            var request = new UpdateSectionRequest
            {
                Id = _testSection.Id,
                Name = _testSection.Name,
            };

            // Act
            var result = await _controller.Update(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _sectionServiceMock.Verify(x => x.Update(request), Times.Once);
        }

        [Fact]
        public async Task Delete_Existing_ShouldReturnOkObjectResult()
        {
            // Arrange
            _sectionServiceMock.Setup(x => x.Delete(_testSection.Id))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _controller.Delete(_testSection.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _sectionServiceMock.Verify(x => x.Delete(_testSection.Id), Times.Once);
        }
    }
}