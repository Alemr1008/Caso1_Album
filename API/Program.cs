
var builder = WebApplication.CreateBuilder(args);

// Registrar los controladores de la API.
builder.Services.AddControllers();

// Registrar servicios para explorar los endpoints.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP.
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