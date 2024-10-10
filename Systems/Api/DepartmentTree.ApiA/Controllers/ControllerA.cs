using DepartmentTree.Context;
using DepartmentTree.Services.ServiceA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepartmentTree.ApiA.Controllers;

[Authorize(Policy = "ServiceBPolicy")]
[ApiController]
[Route("api/[controller]")]
public class AControllerA : ControllerBase
{
    private readonly IServiceA serviceA;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    public AControllerA(IServiceA serviceA, IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.serviceA = serviceA;
        this.dbContextFactory = dbContextFactory;
    }

    [HttpGet]
    public async Task<UnitStatusModel> GetStatusById( int Id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var units = await context.Units.ToListAsync();
        var u = units.FirstOrDefault(x => x.Id == Id);

        if (u == null)
        {
            Console.WriteLine($"Unit with Id {Id} not found.");
            return null;
        }
        return new UnitStatusModel() { Status = u.Status };
    }
}
