using Model.Entities;
using Repository;
using Repository.Repositories;

namespace Test.Repositories;

/// <summary>
/// Uses transaction to reset database after each test.
/// 
/// Never quite sure when clearing tracked entities is necessary. The tests passed without it.
/// I've cleared in a couple tests as an example.
/// </summary>
public class PostRepositoryTest : IDisposable
{
    private readonly DemoContext _context;
    private readonly IPostRepository _postRepository;

    public PostRepositoryTest()
    {
        _context = new ContextFactory().CreateDbContext();
        _postRepository = new PostRepository(_context);
        
        _context.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _context.Database.RollbackTransaction();
        _context.Dispose();
    }

    [Fact]
    public void GivenPostInDb_WhenGetById_ThenReturnPost()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Description = "Test Description",
            Content = "Test Content"
        };
        _context.Posts.Add(post);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        var result = _postRepository.GetById(post.Id);

        // Assert
        Assert.Equal(post.Id, result?.Id);
    }
    
    [Fact]
    public void GivenPostNotInDb_WhenGetById_ThenReturnNull()
    {
        // Arrange
        const int id = 1;
        
        // Act
        var result = _postRepository.GetById(id);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void GivenPostInDb_WhenGetAll_ThenReturnPosts()
    {
        // Arrange
        var post1 = new Post
        {
            Title = "Test Post 1",
            Description = "Test Description 1",
            Content = "Test Content 1"
        };
        var post2 = new Post
        {
            Title = "Test Post 2",
            Description = "Test Description 2",
            Content = "Test Content 2"
        };
        _context.Posts.Add(post1);
        _context.Posts.Add(post2);
        _context.SaveChanges();

        // Act
        var result = _postRepository.GetAll();

        // Assert
        var enumerable = result.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Contains(post1, enumerable);
        Assert.Contains(post2, enumerable);
    }
    
    [Fact]
    public void GivenPostInDb_WhenCount_ThenReturnCount()
    {
        // Arrange
        var post1 = new Post
        {
            Title = "Test Post 1",
            Description = "Test Description 1",
            Content = "Test Content 1"
        };
        var post2 = new Post
        {
            Title = "Test Post 2",
            Description = "Test Description 2",
            Content = "Test Content 2"
        };
        _context.Posts.Add(post1);
        _context.Posts.Add(post2);
        _context.SaveChanges();

        // Act
        var result = _postRepository.Count();

        // Assert
        Assert.Equal(2, result);
    }
    
    [Fact]
    public void GivenPostInDb_WhenUpdate_ThenPostUpdated()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Description = "Test Description",
            Content = "Test Content"
        };
        _context.Posts.Add(post);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        
        // Act
        post.Title = "Updated Title";
        _postRepository.Update(post);
        _postRepository.Save();
        _context.ChangeTracker.Clear();
        
        // Assert
        var result = _context.Posts.Find(post.Id);
        Assert.Equal("Updated Title", result?.Title);
    }
    
    [Fact]
    public void GivenPostInDb_WhenDelete_ThenPostDeleted()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Description = "Test Description",
            Content = "Test Content"
        };
        _context.Posts.Add(post);
        _context.SaveChanges();
        
        // Act
        _postRepository.Delete(post);
        _postRepository.Save();
        
        // Assert
        var result = _postRepository.GetById(post.Id);
        Assert.Null(result);
    }
    
    [Fact]
    public void GivenPostInDb_WhenDeleteById_ThenPostDeleted()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Description = "Test Description",
            Content = "Test Content"
        };
        _context.Posts.Add(post);
        _context.SaveChanges();
        
        // Act
        _postRepository.Delete(post.Id);
        _postRepository.Save();
        
        // Assert
        var result = _postRepository.GetById(post.Id);
        Assert.Null(result);
    }
    
    [Fact]
    public void GivenPostNotInDb_WhenAdd_ThenPostAdded()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Description = "Test Description",
            Content = "Test Content"
        };
        
        // Act
        _postRepository.Add(post);
        _postRepository.Save();
        
        // Assert
        var result = _context.Posts.Find(post.Id);
        Assert.Equal(post.Id, result?.Id);
    }
    
    [Fact]
    public async Task GivenPostNotInDb_WhenAddAsync_ThenPostAdded()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Description = "Test Description",
            Content = "Test Content"
        };
        
        // Act
        await _postRepository.AddAsync(post);
        await _postRepository.SaveAsync();
        
        // Assert
        var result = await _context.Posts.FindAsync(post.Id);
        Assert.Equal(post.Id, result?.Id);
    }
}