using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IArtistaFlujo
    {
        Task<IEnumerable<ArtistaResponse>> ObtenerArtistas();
        Task<ArtistaResponse?> ObtenerArtistaPorId(Guid id);
        Task<Guid> CrearArtista(ArtistaRequest artista);
        Task<bool> ActualizarArtista(Guid id, ArtistaRequest artista);
        Task<bool> EliminarArtista(Guid id);
    }
}
