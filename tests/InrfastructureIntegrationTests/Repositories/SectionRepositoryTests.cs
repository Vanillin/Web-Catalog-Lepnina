using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureIntegrationTests.Repositories;

[Collection("IntegrationTests")]
public class SectionRepositoryTests : IClassFixture<TestingFixture>
{
    private readonly TestingFixture _fixture;
    private readonly IRepositSection _sectionRepository;

    public SectionRepositoryTests(TestingFixture fixture)
    {
        _fixture = fixture;
        var scope = _fixture.ServiceProvider.CreateScope();
        _sectionRepository = scope.ServiceProvider.GetRequiredService<IRepositSection>();
    }

    [Fact]
    public async Task Delete_ValidRequest_DeletesSection()
    {
        // Arrange
        var section = await _fixture.CreateSection();

        // Act
        var result = await _sectionRepository.Delete(section.Id);
        var exists = await _sectionRepository.ReadById(section.Id);

        // Assert
        result.Should().BeTrue();
        exists.Should().BeNull();
    }

    [Fact]
    public async Task ReadById_WhenExists_ReturnsTrue()
    {
        // Arrange
        var section = await _fixture.CreateSection();

        // Act
        var exists = await _sectionRepository.ReadById(section.Id);

        // Assert
        exists.Should().NotBeNull();
    }

    [Fact]
    public async Task ReadById_WhenNotExists_ReturnsFalse()
    {
        // Arrange

        // Act
        var exists = await _sectionRepository.ReadById(1);

        // Assert
        exists.Should().BeNull();
    }
}