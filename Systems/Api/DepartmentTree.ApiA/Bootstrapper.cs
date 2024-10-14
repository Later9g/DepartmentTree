using DepartmentTree.Services.Settings;
using DepartmentTree.Services.ServiceA;
using DepartmentTree.Services.ServiceB;

namespace DepartmentTree.Services.ServiceA;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddServiceA()
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings();

        return services;
    }
}
