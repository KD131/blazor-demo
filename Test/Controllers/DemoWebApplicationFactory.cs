using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Controllers;

/// <summary>
///     Configures web API for testing. Uses the test database. If mocking services is desired, do that in the test class.
/// </summary>
/// <typeparam name="TProgram">Startup class</typeparam>
public class DemoWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Production");
        builder.ConfigureServices(services =>
        {
            var contextOptions = ContextFactory.GetOptions();
            services.AddScoped(_ => contextOptions);
        });
    }
}