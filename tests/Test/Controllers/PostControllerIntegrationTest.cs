using System.Net;
using System.Net.Http.Json;
using API;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit.Abstractions;

namespace Test.Controllers;

public class PostControllerIntegrationTest : IClassFixture<DemoWebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IPostService> _postService;
    private readonly ITestOutputHelper _testOutputHelper;

    public PostControllerIntegrationTest(DemoWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
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
        _testOutputHelper.WriteLine(_factory.ClientOptions.BaseAddress.Port.ToString());
        // Arrange
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://example.com:9999")
        });
        _testOutputHelper.WriteLine(client.BaseAddress!.Port.ToString());
        var post = new { };

        _testOutputHelper.WriteLine(_factory.Server.BaseAddress.Port.ToString());

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