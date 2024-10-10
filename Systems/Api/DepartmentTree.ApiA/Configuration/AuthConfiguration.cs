using Microsoft.IdentityModel.Tokens;

namespace DepartmentTree.ApiA.Configuration;

public class AuthConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5000"; // IdentityServer4 URL
                options.TokenValidationParameters = new TokenValidationParameters
                {
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

    public void Configure(IApplicationBuilder app)
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
