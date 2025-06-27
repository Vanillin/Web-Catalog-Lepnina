using Application.Exception;
using Application.Request;
using Application.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationIntegrationTests.Services;

[Collection("IntegrationTests")]
public class SectionServiceTests : IClassFixture<TestingFixture>
{
    private readonly TestingFixture _fixture;
    private readonly IServiceSection _sectionService;
    //private readonly IPostService _postService;

    public SectionServiceTests(TestingFixture fixture)
    {
        _fixture = fixture;
        var scope = _fixture.ServiceProvider.CreateScope();
        _sectionService = scope.ServiceProvider.GetRequiredService<IServiceSection>();
        // _postService = scope.ServiceProvider.GetRequiredService<IPostService>();
    }

    [Fact]
    public async Task ReadAll_ShouldReturnSection()
    {
        // Arrange
        var section = await _fixture.CreateSection();
        var request = new CreateSectionRequest
        {
            Name = section.Name,
        };
        await _sectionService.Create(request);

        // Act
        var comments = (await _sectionService.ReadAll()).ToList();

        // Assert
        comments.Should().HaveCountGreaterThan(1);
    }

    [Fact]
    public async Task Update_ShouldUpdateExistingSection()
    {
        // Arrange
        var section = await _fixture.CreateSection();
        var request = new CreateSectionRequest
        {
            Name = section.Name,
        };
        var commentId = await _sectionService.Create(request);

        var updateRequest = new UpdateSectionRequest
        {
            Id = (int)commentId,
            Name = "update" + section.Name,
        };

        // Act
        await _sectionService.Update(updateRequest);

        // Assert
        var updatedComment = await _sectionService.ReadById((int)commentId);
        updatedComment.Should().NotBeNull();
        updatedComment.Name.Should().Be(updateRequest.Name);
    }

    [Fact]
    public async Task Delete_ShouldRemoveSection()
    {
        // Arrange
        var section = await _fixture.CreateSection();
        var request = new CreateSectionRequest
        {
            Name = section.Name,
        };
        var commentId = await _sectionService.Create(request);

        // Act
        await _sectionService.Delete((int)commentId);

        // Assert
        await _sectionService.Invoking(x => x.ReadById((int)commentId))
            .Should().ThrowAsync<EntityNotFoundException>()
            .WithMessage($"Section is not found");
    }
}