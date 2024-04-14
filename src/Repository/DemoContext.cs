using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Repository;

public class DemoContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    
    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
    }
}