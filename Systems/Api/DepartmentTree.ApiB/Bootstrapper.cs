using DepartmentTree.Services.ServiceA;
using DepartmentTree.Services.Settings;

namespace DepartmentTree.Services.ServiceB;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddServiceA()
            .AddServiceB()
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings();

        return services;
    }
}
