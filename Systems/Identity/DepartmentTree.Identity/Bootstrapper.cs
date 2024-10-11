using DepartmentTree.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentTree.Identity;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings();

        return services;
    }
}
