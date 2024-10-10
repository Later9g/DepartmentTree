namespace DepartmnetTree.Services.ServiceB;

public static class Bootstrapper
{
    public static IServiceCollection AddServiceA(this IServiceCollection services)
    {
        services.AddServiceA();

        return services;
    }
}
