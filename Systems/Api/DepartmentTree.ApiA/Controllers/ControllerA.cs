using Asp.Versioning;
using DepartmentTree.Context;
using DepartmentTree.Services.ServiceA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepartmentTree.ApiB.Controllers;

[Authorize(Policy = "ServiceBPolicy")]
[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "ControllerA")]
public class ControllerA : ControllerBase
{
    private readonly IServiceA serviceA;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    public ControllerA(IServiceA serviceA, IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.serviceA = serviceA;
        this.dbContextFactory = dbContextFactory;
    }

    [HttpGet()]
    public async Task<IActionResult> GetStatusById([FromQuery] int id)
    {
        var status = await serviceA.GetUnitStatusByIdAsync(id);

        if (status == null)
        {
            return NotFound($"Unit with Id {id} not found.");
        }

        return Ok(status);
    }
}
