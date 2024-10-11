using Microsoft.IdentityModel.Tokens;

namespace DepartmentTree.ApiA.Configuration;

public static class AuthConfiguration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5146"; // IdentityServer4 URL
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:5146",
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ServiceBPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "service_a");
            });
        });

        services.AddControllers();
    }

    public static void Configure(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}
