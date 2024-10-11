using DepartmentTree.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DepartmentTree.Services.ServiceA;

public class ServiceA : IServiceA
{
    private readonly HttpClient httpClient;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    public ServiceA (HttpClient httpClient, IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.httpClient = httpClient;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<UnitStatusModel> GetUnitStatusByIdAsync(int id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var unit = await context.Units.FirstOrDefaultAsync(x => x.Id == id);

        if (unit == null)
        {
            Console.WriteLine($"Unit with Id {id} not found.");
            return null;
        }
        return new UnitStatusModel() { Status = unit.Status };
    }
}
