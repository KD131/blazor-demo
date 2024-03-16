using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace Test;

public class ContextFactory : IDesignTimeDbContextFactory<DemoContext>
{
    public DemoContext CreateDbContext(string[] args)
    {
        DotNetEnv.Env.TraversePath().Load();
        var optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
        optionsBuilder.UseSqlServer(DotNetEnv.Env.GetString("SQLCONNSTR_DEMO_TEST"));
        return new DemoContext(optionsBuilder.Options);
    }
}