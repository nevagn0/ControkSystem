using ControkSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ControkSystem.Application.Services;
using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Infrastructure.Repositories;
using System.Diagnostics;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Control System API", 
        Version = "v1"
    });
});

builder.Services.AddDbContext<ControlSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<IDefectRepository, DefectRepository>();
builder.Services.AddScoped<DefectServices>();
builder.Services.AddScoped<IProjectrepository, ProjectRepository>();
builder.Services.AddScoped<ProjectServices>();
var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Control System API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    _ = Task.Run(async () =>
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "http://localhost:5115/swagger",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось открыть браузер: {ex.Message}");
        }
    });

    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
}