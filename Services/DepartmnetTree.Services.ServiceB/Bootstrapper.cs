using Microsoft.Extensions.DependencyInjection;

namespace DepartmentTree.Services.ServiceB;

public static class Bootstrapper
{
    public static IServiceCollection AddServiceB(this IServiceCollection services)
    {
        services.AddScoped<IServiceB, ServiceB>();

        return services;
    }
}
