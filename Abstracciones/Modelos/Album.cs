
namespace Abstracciones.Modelos
{
    // Propiedades comunes de los álbumes.
    public class AlbumBase
    {
        public string Titulo { get; set; } = string.Empty;
        public string Artista { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int AnioLanzamiento { get; set; }
        public int CantidadCanciones { get; set; }
    }

    // Datos recibidos en POST y PUT.
    public class AlbumRequest : AlbumBase
    {
    }

    // Datos que devuelve la API.
    public class AlbumResponse : AlbumBase
    {
        public Guid Id { get; set; }
    }
}
