using DepartmentTree.Services.Settings;

namespace DepartmnetTree.Services.ServiceB;

public static class Bootstrapper
{
    public static IServiceCollection AddServiceA(this IServiceCollection services)
    {
        services
            .AddServiceB()
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings();

        return services;
    }
}
