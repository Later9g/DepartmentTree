namespace DepartmentTree.Identity.Configuration;
public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services.AddIdentityServer()
            .AddInMemoryClients(Config.GetClients())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddDeveloperSigningCredential(); // Для локальной разработки

        services.AddControllers();

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseIdentityServer();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}

