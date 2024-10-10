namespace DepartmentTree.Api;

using DepartmentTree.Services.Logger;
using DepartmentTree.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddAppLogger()
            ;
            

        return service;
    }
}
