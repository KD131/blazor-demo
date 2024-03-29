using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace Test;

public class ContextFactory : IDesignTimeDbContextFactory<DemoContext>
{
    public DemoContext CreateDbContext(string[]? args = null)
    {
        return new DemoContext(GetOptions());
    }
    
    public static DbContextOptions<DemoContext> GetOptions()
    {
        DotNetEnv.Env.TraversePath().Load();
        var optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
        optionsBuilder.UseSqlServer(DotNetEnv.Env.GetString("SQLCONNSTR_DEMO_TEST"));
        return optionsBuilder.Options;
    }
}