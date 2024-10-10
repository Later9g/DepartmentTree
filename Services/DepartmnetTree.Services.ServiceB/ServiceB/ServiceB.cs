using DepartmentTree.Context;
using DepartmentTree.Context.Entities;
using DepartmentTree.Services.ServiceA;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DepartmnetTree.Services.ServiceB;

public class ServiceB : IServiceB
{
    private readonly HttpClient httpClient;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    private readonly ServiceA serviceA;

    public ServiceB(HttpClient httpClient, IDbContextFactory<AppDbContext> dbContextFactory,ServiceA serviceA)
    {
        this.httpClient = httpClient;
        this.dbContextFactory = dbContextFactory;
        this.serviceA = serviceA;
    }

    public async Task<UnitStatusModel> GetStatusesAsync(int Id)
    {

        var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5000");
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

        var response = await httpClient.GetAsync($"http://localhost:5250/api/status/?Id={Id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UnitStatusModel>(content);
    }

    public async Task<IEnumerable<OtherUnitModel>> GetUnitsAsync()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var units = await context.Units.ToListAsync();

        var result = new List<OtherUnitModel>();

        foreach (var x in units)
        {
            var status = await GetStatusesAsync(x.Id);
            result.Add(new OtherUnitModel()
            {
                Id = x.Id,
                Status = status.Status,
                Name = x.Name,
                ParentId = x.ParentId,
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
