using DepartmentTree.Services.ServiceA;
using System.Text.Json;

namespace DepartmentTree.Services.ServiceA;

public class ServiceA : IServiceA
{
    private readonly HttpClient httpClient;

    public ServiceA (HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<UnitStatusModel>> GetStatusesAsync()
    {
        var response = await httpClient.GetAsync("api/status");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<UnitStatusModel>>(content);
    }
}
