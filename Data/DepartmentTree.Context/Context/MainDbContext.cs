
using DepartmentTree.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DepartmentTree.Context;
public class MainDbContext : DbContext
{
    public DbSet<Unit> Units { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
        modelBuilder.Entity<Unit>().ToTable("Units");

    }
}
