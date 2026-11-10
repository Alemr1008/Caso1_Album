using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Flujo;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Registrar los controladores de la API.
builder.Services.AddControllers();

// Registrar servicios para explorar los endpoints.
builder.Services.AddEndpointsApiExplorer();

// Inyección de dependencias: flujo y acceso a datos (Álbumes)
builder.Services.AddScoped<IAlbumFlujo, AlbumFlujo>();
builder.Services.AddScoped<IAlbumDA, AlbumDA>();

// Inyección de dependencias: flujo y acceso a datos (Artistas - Minimal API)
// Nota: Asegúrate de crear también estas interfaces y clases en sus respectivas capas.
builder.Services.AddScoped<IArtistaFlujo, ArtistaFlujo>();
builder.Services.AddScoped<IArtistaDA, ArtistaDA>();

var app = builder.Build();

// Configuración del pipeline HTTP.
if (app.Environment.IsDevelopment())
{
<<<<<<< Updated upstream
    app.UseHsts();
=======
    app.UseSwagger();
    app.UseSwaggerUI();
>>>>>>> Stashed changes
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

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

// Mapear los controladores tradicionalistas de la API (como AlbumController).
app.MapControllers();

app.Run();
