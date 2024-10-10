using Microsoft.Extensions.DependencyInjection;

namespace DepartmentTree.Services.ServiceA;

public static class Bootstrapper
{
 
    public static IServiceCollection AddServiceA(this IServiceCollection services)
    {
        services.AddScoped<IServiceA, ServiceA>();

        return services;
    }
}
