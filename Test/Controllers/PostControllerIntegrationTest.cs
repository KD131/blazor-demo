using System.Net;
using System.Net.Http.Json;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Test.Controllers;

public class PostControllerIntegrationTest : IClassFixture<DemoWebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IPostService> _postService;

    public PostControllerIntegrationTest(DemoWebApplicationFactory<Program> factory)
    {
        _postService = new Mock<IPostService>();
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Mock services here
                services.AddScoped(_ => _postService.Object);
            });
        });
    }

    [Fact]
    public async Task GivenBadInput_WhenCreatePost_ThenReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var post = new { };

        // Act
        var response = await client.PostAsJsonAsync("api/Post", post);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        var errors = problemDetails!.Errors;

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal((int)HttpStatusCode.BadRequest, problemDetails.Status);
        Assert.Equal("The Title field is required.", errors["Title"][0]);
        Assert.Equal("The Content field is required.", errors["Content"][0]);
    }
}