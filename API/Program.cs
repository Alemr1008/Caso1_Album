
var builder = WebApplication.CreateBuilder(args);

// Registrar los controladores de la API.
builder.Services.AddControllers();

// Registrar servicios para explorar los endpoints.
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configuración del pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear los controladores de la API.
app.MapControllers();

app.Run();
