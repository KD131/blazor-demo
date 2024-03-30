using Model.Dtos;
using Model.Entities;
using Moq;
using Shared.Services;

namespace Test.Services;

public class PostServiceTest(PostServiceFixture fixture) : IClassFixture<PostServiceFixture>
{

    [Fact]
    public async Task GivenPostExists_WhenGetById_ThenReturnDto()
    {
        // Arrange
        var post = new Post { Id = 1, Title = "Test", Content = "Test" };
        fixture.PostRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(post).Verifiable();

        // Act
        var result = await fixture.PostService.GetPostByIdAsync(1);

        // Assert
        fixture.PostRepository.Verify();
        Assert.Equal(post, result);
    }
    
    [Fact]
    public async Task GivenPostsExist_WhenGetAll_ThenReturnDtos()
    {
        // Arrange
        var posts = new List<Post>
        {
            new Post { Id = 1, Title = "Test", Content = "Test" },
            new Post { Id = 2, Title = "Test", Content = "Test" }
        };
        fixture.PostRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(posts).Verifiable();

        // Act
        var result = await fixture.PostService.GetPostsAsync();

        // Assert
        fixture.PostRepository.Verify();
        Assert.Equal(posts, result);
    }
    
    [Fact]
    public async Task GivenPostDoesNotExist_WhenGetById_ThenThrowKeyNotFoundException()
    {
        // Arrange
        const int id = 1;
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Post?)null).Verifiable();

        // Act
        async Task Act() => await fixture.PostService.GetPostByIdAsync(id);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(Act);
        fixture.PostRepository.Verify();
    }
    
    [Fact]
    public async Task GivenPostDoesNotExist_WhenUpdate_ThenThrowKeyNotFoundException()
    {
        // Arrange
        const int id = 1;
        var post = new PostDto { Id = id, Title = "Test", Content = "Test" };
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Post?)null).Verifiable();

        // Act
        async Task Act() => await fixture.PostService.UpdatePostAsync(post);

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
        await fixture.PostService.UpdatePostAsync(post);

        // Assert
        fixture.PostRepository.Verify();
        fixture.PostRepository.Verify(x => x.Update(It.Is<Post>(e => e.Id == id)));
    }
    
    [Fact]
    public async Task GivenPostExists_WhenDelete_ThenDeletePost()
    {
        // Arrange
        const int id = 1;
        var post = new Post { Id = id, Title = "Test", Content = "Test" };
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(post).Verifiable();

        // Act
        await fixture.PostService.DeletePostAsync(id);

        // Assert
        fixture.PostRepository.Verify();
        fixture.PostRepository.Verify(x => x.Delete(post));
    }
    
    [Fact]
    public async Task GivenPostDoesNotExist_WhenDelete_ThenThrowKeyNotFoundException()
    {
        // Arrange
        const int id = 1;
        fixture.PostRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Post?)null).Verifiable();

        // Act
        async Task Act() => await fixture.PostService.DeletePostAsync(id);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(Act);
        fixture.PostRepository.Verify();
    }
}