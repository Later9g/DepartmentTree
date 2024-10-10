
using DepartmentTree.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartmentTree.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ConfigureUnits();
        base.OnModelCreating(modelBuilder);
    }
}
