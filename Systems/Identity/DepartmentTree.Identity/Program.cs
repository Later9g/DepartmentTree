using DepartmentTree.Context;
using DepartmentTree.Identity;
using DepartmentTree.Identity.Configuration;
using DepartmentTree.Services.Settings;
using DepartmentTree.Settings;
using Microsoft.EntityFrameworkCore;

var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(logSettings);

// Configure services
var services = builder.Services;

services.AddAppCors();

services.AddHttpContextAccessor();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddAppHealthChecks();

services.RegisterAppServices();

services.AddIS4();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseIS4();

app.Run();