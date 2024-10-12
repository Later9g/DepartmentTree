using DepartmentTree.Services.Settings;
using DepartmentTree.Settings;
using DepartmentTree.Context;
using Microsoft.EntityFrameworkCore;
using DepartmentTree.ApiA.Configuration;
using DepartmentTree.Services.ServiceA;
using DepartmanetTree.ApiA.Configuration;

var builder = WebApplication.CreateBuilder(args);

var mainSetting = Settings.Load<MainSettings>("Main");
var logSetting = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

builder.AddAppLogger(mainSetting, logSetting);

// Add services to the container.

var services = builder.Services;

services.AddAppCors();
services.AddAppVersioning();
services.AddHttpClient();
services.AddHttpContextAccessor();
services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddAppHealthChecks();
services.RegisterAppServices();
services.AddAppSwagger(mainSetting, swaggerSettings, identitySettings);
services.ConfigureServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAppCors();
app.Configure();
app.UseAppSwagger();
app.UseAppHealthChecks();
app.UseAuthorization();
app.MapControllers();

app.Run();