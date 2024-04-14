using Model.Dtos;
using Model.Entities;
using Moq;

namespace Test.Services;

public class PostServiceTest(MockPostRepositoryFixture fixture) : IClassFixture<MockPostRepositoryFixture>
{
    [Fact]
    public async Task GivenPostExists_WhenGetById_ThenReturnDto()
    {
        // Arrange
        var post = new Post { Id = 1, Title = "Test", Content = "Test" };
        fixture.PostRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(post).Verifiable();

        // Act
        var result = await fixture.PostService.GetByIdAsync(1);

        // Assert
        fixture.PostRepository.Verify();
        Assert.Equal(post.Id, result.Id);
    }

    [Fact]
    public async Task GivenPostsExist_WhenGetAll_ThenReturnDtos()
    {
        // Arrange
        var posts = new List<Post>
        {
            new() { Id = 1, Title = "Test", Content = "Test" },
            new() { Id = 2, Title = "Test", Content = "Test" }
        };
        fixture.PostRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(posts).Verifiable();

        // Act
        var result = await fixture.PostService.GetAllAsync();

        // Assert
        fixture.PostRepository.Verify();
        Assert.Equal(posts.Count, result.Count());
    }

    [Fact]
    public async Task GivenPostDoesNotExist_WhenGetById_ThenThrowKeyNotFoundException()
    {
        // Arrange
        const int id = 1;
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Post?)null).Verifiable();

        // Act
        async Task Act()
        {
            await fixture.PostService.GetByIdAsync(id);
        }

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(Act);
        fixture.PostRepository.Verify();
    }

    [Fact]
    public async Task GivenPostDoesNotExist_WhenCreate_ThenCreatePost()
    {
        // Arrange
        var post = new PostDto { Title = "Test", Content = "Test" };
        var entity = fixture.Mapper.Map<Post>(post);

        // Act
        var result = await fixture.PostService.CreateAsync(post);

        // Assert
        fixture.PostRepository.Verify(x => x.AddAsync(It.IsAny<Post>()));
        fixture.PostRepository.Verify(x => x.SaveAsync());
    }

    [Fact]
    public async Task GivenPostDoesNotExist_WhenUpdate_ThenThrowKeyNotFoundException()
    {
        // Arrange
        const int id = 1;
        var post = new PostDto { Id = id, Title = "Test", Content = "Test" };
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Post?)null).Verifiable();

        // Act
        async Task Act()
        {
            await fixture.PostService.UpdateAsync(id, post);
        }

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(Act);
        fixture.PostRepository.Verify();
    }

    [Fact]
    public async Task GivenPostExists_WhenUpdate_ThenUpdatePost()
    {
        // Arrange
        const int id = 1;
        var post = new PostDto { Id = id, Title = "Test", Content = "Test" };
        var entity = fixture.Mapper.Map<Post>(post);
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(entity).Verifiable();

        // Act
        await fixture.PostService.UpdateAsync(id, post);

        // Assert
        fixture.PostRepository.Verify();
        fixture.PostRepository.Verify(x => x.Update(It.Is<Post>(e => e.Id == id)));
        fixture.PostRepository.Verify(x => x.SaveAsync());
    }

    [Fact]
    public async Task GivenPostExists_WhenDelete_ThenDeletePost()
    {
        // Arrange
        const int id = 1;
        var post = new Post { Id = id, Title = "Test", Content = "Test" };
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(post).Verifiable();

        // Act
        await fixture.PostService.DeleteAsync(id);

        // Assert
        fixture.PostRepository.Verify();
        fixture.PostRepository.Verify(x => x.Delete(post));
        fixture.PostRepository.Verify(x => x.SaveAsync());
    }

    [Fact]
    public async Task GivenPostDoesNotExist_WhenDelete_ThenThrowKeyNotFoundException()
    {
        // Arrange
        const int id = 1;
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Post?)null).Verifiable();

        // Act
        async Task Act()
        {
            await fixture.PostService.DeleteAsync(id);
        }

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(Act);
        fixture.PostRepository.Verify();
    }
}