
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using DA;
using Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
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

// --- MINIMAL API DE ARTISTAS ---
var artistasItems = app.MapGroup("/api/artistas");

// GET /api/artistas (200 OK)
artistasItems.MapGet("/", async (IArtistaFlujo artistaFlujo) =>
{
    var artistas = await artistaFlujo.ObtenerArtistas();
    return Results.Ok(artistas);
});

// GET /api/artistas/{id} (200 OK o 404 Not Found)
artistasItems.MapGet("/{id:guid}", async (Guid id, IArtistaFlujo artistaFlujo) =>
{
    var artista = await artistaFlujo.ObtenerArtistaPorId(id);
    return artista is not null ? Results.Ok(artista) : Results.NotFound();
});

// POST /api/artistas (201 Created)
artistasItems.MapPost("/", async ([FromBody] ArtistaRequest request, IArtistaFlujo artistaFlujo) =>
{
    var id = await artistaFlujo.CrearArtista(request);
    var artistaCreado = await artistaFlujo.ObtenerArtistaPorId(id);
    return Results.Created($"/api/artistas/{id}", artistaCreado);
});

// PUT /api/artistas/{id} (204 No Content o 404 Not Found)
artistasItems.MapPut("/{id:guid}", async (Guid id, [FromBody] ArtistaRequest request, IArtistaFlujo artistaFlujo) =>
{
    var actualizado = await artistaFlujo.ActualizarArtista(id, request);
    return actualizado ? Results.NoContent() : Results.NotFound();
});

// DELETE /api/artistas/{id} (204 No Content o 404 Not Found)
artistasItems.MapDelete("/{id:guid}", async (Guid id, IArtistaFlujo artistaFlujo) =>
{
    var eliminado = await artistaFlujo.EliminarArtista(id);
    return eliminado ? Results.NoContent() : Results.NotFound();
});
// -------------------------------

// Mapear los controladores de la API.
app.MapControllers();

app.Run();
