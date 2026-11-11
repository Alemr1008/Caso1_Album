
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using DA;
using Flujo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURACIÓN DE CONTROLADORES Y SWAGGER
// ==========================================

// Registrar los controladores de la API.
builder.Services.AddControllers();

// Registrar servicios para explorar los endpoints.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================================
// CONFIGURACIÓN DE ENTITY FRAMEWORK CORE
// ==========================================

// Registrar el contexto de base de datos SQLite.
builder.Services.AddDbContext<VinilosDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("Vinilos")
    )
);

// ==========================================
// INYECCIÓN DE DEPENDENCIAS
// ==========================================

// Registrar servicios del Caso 01.
builder.Services.AddScoped<IAlbumFlujo, AlbumFlujo>();
builder.Services.AddScoped<IAlbumDA, AlbumDA>();

// Registrar servicios de Artista.
builder.Services.AddScoped<IArtistaFlujo, ArtistaFlujo>();
builder.Services.AddScoped<IArtistaDA, ArtistaDA>();

// ==========================================
// CONSTRUCCIÓN DE LA APLICACIÓN
// ==========================================

var app = builder.Build();

// ==========================================
// MIGRACIONES AUTOMÁTICAS DE ENTITY FRAMEWORK
// ==========================================

// Crear un ámbito de servicios para obtener el DbContext.
using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider
        .GetRequiredService<VinilosDbContext>();

    // Aplicar las migraciones pendientes a SQLite.
    await contexto.Database.MigrateAsync();
}

// ==========================================
// CONFIGURACIÓN DEL PIPELINE HTTP
// ==========================================

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Archivos estáticos.
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

// Mapear los controladores de la API.
app.MapControllers();

app.Run();
