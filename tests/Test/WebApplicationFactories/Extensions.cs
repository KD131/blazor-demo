using Microsoft.Extensions.DependencyInjection;

namespace Test.WebApplicationFactories;

public static class Extensions
{
    /// <summary>
    /// Adds test DB to services by overriding the context options. Gets options from the context factory for EF migrations.
    /// </summary>
    public static IServiceCollection AddTestDb(this IServiceCollection services)
    {
        services.AddScoped(_ => ContextFactory.GetOptions());
        return services;
    }
}