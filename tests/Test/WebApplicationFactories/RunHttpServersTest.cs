using API;
using Microsoft.AspNetCore.Mvc.Testing;
using Test.Controllers;
using Xunit.Abstractions;

namespace Test.WebApplicationFactories;

public class RunHttpServersTest
{
    private readonly WebApplicationFactory<Program> _apiFactory = new DemoWebApplicationFactory<Program>();
    private readonly WebApplicationFactory<BlazorDemo.Program> _blazorFactory = new();
    private readonly ITestOutputHelper _testOutputHelper;

    public RunHttpServersTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine(_apiFactory.Server.BaseAddress.Port.ToString());
        _testOutputHelper.WriteLine(_blazorFactory.Server.BaseAddress.Port.ToString());
    }
    
    [Fact]
    public async Task Startup()
    {
        var apiClient = _apiFactory.CreateClient();
        var blazorClient = _blazorFactory.CreateClient();
        await blazorClient.GetAsync("/");
    }
}