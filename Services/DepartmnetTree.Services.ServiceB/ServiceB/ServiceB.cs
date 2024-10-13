using DepartmentTree.Context;
using DepartmentTree.Context.Entities;
using DepartmentTree.Services.ServiceA;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DepartmentTree.Services.ServiceB;

public class ServiceB : IServiceB
{
    private readonly HttpClient httpClient;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    private readonly IServiceA serviceA;
    private readonly ILogger logger;

    public ServiceB(HttpClient httpClient, IDbContextFactory<AppDbContext> dbContextFactory,IServiceA serviceA,ILogger logger)
    {
        this.httpClient = httpClient;
        this.dbContextFactory = dbContextFactory;
        this.serviceA = serviceA;
        this.logger = logger;
    }

    public async Task<UnitStatusModel> GetStatusesAsync(int id)
    {

        var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5146");

        if (discoveryDocument.IsError)
        {
            throw new Exception($"Discovery document error: {discoveryDocument.Error}");
        }

        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = "service_b",
            ClientSecret = "secret",
            Scope = "service_a"
        });

        if (tokenResponse.IsError)
        {
            throw new Exception("Failed to retrieve token.");
        }

        httpClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await httpClient.GetAsync($"http://localhost:5136/api/ControllerA/?id={id}");

        if (!response.IsSuccessStatusCode)
        {
            logger.Information($"Failed to retrieve status for ID {id}: {response.StatusCode}");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        logger.Information($"Response content for ID {id}: {content}");
        var status = JsonSerializer.Deserialize<UnitStatusModel>(content);

        if (status == null)
        {
            logger.Information($"Status for ID {id} is null.");
            return null;
        }

        return status;
    }

    public async Task<IEnumerable<OtherUnitModel>> GetUnitsAsync()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var units = await context.Units.ToListAsync();

        var result = new List<OtherUnitModel>();

        foreach (var unit in units)
        {
            var status = await GetStatusesAsync(unit.Id);
            if (status == null)
            {
                logger.Information($"No status found for unit ID {unit.Id}. Skipping...");
                continue;
            }

            result.Add(new OtherUnitModel()
            {
                Id = unit.Id,
                Status = status.Status,
                Name = unit.Name,
                ParentId = unit.ParentId,
            });
        }

        return result;
    }

    public async Task UpdateUnitAsync(int id, UpdateModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var unit = await context.Units.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (unit != null)
        {
            unit.ParentId = model.ParentId;
            context.Units.Update(unit);
            await context.SaveChangesAsync();
        }
    }

    public async Task SyncUnitsAsync(IEnumerable<OtherUnitModel> models)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var existingUnits = await GetUnitsAsync();

        foreach (var model in models)
        {
            bool updated = false;

            foreach (var unit in existingUnits)
            {
                if (unit.Id == model.Id && unit.Name == model.Name && unit.Status == model.Status && unit.ParentId != model.ParentId)
                {
                    await UpdateUnitAsync(model.Id, new UpdateModel() { ParentId = model.ParentId });
                    updated = true;
                }
            }

            if (!updated && !existingUnits.Any(u => u.Id == model.Id && u.Name == model.Name))
            {
                var newUnit = new Unit()
                {
                    Id = model.Id,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    Status = model.Status
                };
                context.Units.Add(newUnit);
            }
        }

        await context.SaveChangesAsync();
    }
}
