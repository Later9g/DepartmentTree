using DepartmentTree.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartmentTree.Context;

public static class UnitsContextConfiguration
{
    public static void ConfigureUnits(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>().ToTable("units");
    }
}
