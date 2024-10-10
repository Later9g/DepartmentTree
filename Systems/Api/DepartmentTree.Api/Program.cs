using DepartmentTree.Api;
using DepartmentTree.Api.Configuration;
using DepartmentTree.Services.Settings;
using DepartmentTree.Settings;

var builder = WebApplication.CreateBuilder(args);

var mainSetting = Settings.Load<MainSettings>("Main");
var logSetting = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger(mainSetting, logSetting);

var services = builder.Services;

services.AddHttpContextAccessor();
//services.AddAppDbContext();
services.RegisterServices();
services.AddAppCors();  
services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppControllerAndViews();
services.AddAppSwagger(mainSetting, swaggerSettings);

services.RegisterServices(builder.Configuration);
var app = builder.Build();

app.UseAppSwagger();
app.UseAppCors();
app.UseAppControllerAndViews();
app.UseAppHealthChecks();

app.Run();