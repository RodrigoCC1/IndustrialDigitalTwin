using DigitalTwin.Infrastructure.Persistence;
using DigitalTwin.Application.Interfaces;
using DigitalTwin.Application.Services;
using DigitalTwin.Domain.Interfaces;
using DigitalTwin.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN POR DEFECTO (ASPIRE / CLOUD)
// ¡IMPORTANTE! Deja esto. Configura métricas y salud automáticamente.
builder.AddServiceDefaults();

// 2. AGREGAR SERVICIOS AL CONTENEDOR

// Habilitar Controladores (Vital para tu arquitectura Clean Arch)
builder.Services.AddControllers();

// Configuración de Errores y OpenAPI
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Agregamos Swagger visual

// --- A. CONEXIÓN A BASE DE DATOS (SQL SERVER) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- B. INYECCIÓN DE DEPENDENCIAS (Tus Clases) ---
builder.Services.AddScoped<IMotorRepository, MotorRepository>();
builder.Services.AddScoped<IMotorService, MotorService>();

var app = builder.Build();

// 3. CONFIGURACIÓN DEL PIPELINE HTTP

app.UseExceptionHandler();

// Habilitar la pantalla visual de Swagger (Para probar tu API)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // <-- Este método requiere el paquete NuGet Swashbuckle.AspNetCore
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear los controladores que creemos (como el MotorController)
app.MapControllers();

// Endpoints por defecto de Aspire (Salud y Métricas)
app.MapDefaultEndpoints();

app.Run();