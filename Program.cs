using ControkSystem.DBContex;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ControlSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Добавьте это для обслуживания статических файлов
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{

    try
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "http://localhost:5114/index.html",
            UseShellExecute = true
        });

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при открытии браузера: {ex.Message}");
    }
}

app.Run();