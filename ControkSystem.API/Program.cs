using ControkSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ControkSystem.Application.Services;
using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Infrastructure.Repositories;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Database
builder.Services.AddDbContext<ControlSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services and repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Control System API");
        c.RoutePrefix = "swagger";
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Запускаем приложение
var appTask = app.RunAsync();

// Даем время на запуск и открываем браузер
if (app.Environment.IsDevelopment())
{
    await Task.Delay(2000);
    
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "http://localhost:5115/index.html",
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Не удалось открыть браузер: {ex.Message}");
    }
}