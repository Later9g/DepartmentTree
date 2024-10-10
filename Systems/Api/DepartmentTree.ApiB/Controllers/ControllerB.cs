using DepartmnetTree.Services.ServiceB;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentTree.ApiB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ControllerB : ControllerBase
{
    private readonly IServiceB serviceB;

    public ControllerB(IServiceB serviceB)
    {
        this.serviceB = serviceB;
    }

    [HttpGet]
    public async Task<IEnumerable<OtherUnitModel>> GetUnits()
    {
        var result = await serviceB.GetUnitsAsync();
        return result;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateModel model)
    {
        await serviceB.UpdateUnitAsync(id, model);
        return Ok();
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncUnits(IEnumerable<OtherUnitModel> models)
    {
        if (models == null || !models.Any())
        {
            return BadRequest("Модели не могут быть пустыми.");
        }

        await serviceB.SyncUnitsAsync(models);
        return Ok();
    }

}
