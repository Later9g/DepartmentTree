using DepartmentTree.Services.Settings;
using DepartmentTree.Settings;
using DepartmentTree.ApiB.Configuration;
using DepartmentTree.Context;
using Microsoft.EntityFrameworkCore;
using DepartmentTree.Services.ServiceB;
using DepartmanetTree.ApiB.Configuration;

var builder = WebApplication.CreateBuilder(args);

var mainSetting = Settings.Load<MainSettings>("Main");
var logSetting = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

builder.AddAppLogger(mainSetting, logSetting);

// Add services to the container.

var services = builder.Services;

services.AddAppVersioning();
services.AddHttpClient();
services.AddHttpContextAccessor();
services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 
services.AddAppCors();
services.AddAppHealthChecks();
services.RegisterAppServices();
services.AddAppSwagger(mainSetting, swaggerSettings, identitySettings);
services.ConfigureServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.Configure();

app.UseAppSwagger();
app.UseAppCors();
app.UseAppHealthChecks();
app.UseAuthorization();
app.MapControllers();

app.Run();
