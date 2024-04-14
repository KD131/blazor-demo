using API;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Test.Controllers;

public class TwoWebApplications
{
    private readonly WebApplicationFactory<Program> _apiFactory = new DemoWebApplicationFactory<Program>();
    private readonly WebApplicationFactory<BlazorDemo.Program> _blazorFactory = new();
    private readonly ITestOutputHelper _testOutputHelper;

    public TwoWebApplications(ITestOutputHelper testOutputHelper)
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