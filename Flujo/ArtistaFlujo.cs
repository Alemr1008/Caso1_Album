using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ArtistaFlujo : IArtistaFlujo
    {
        private readonly IArtistaDA _artistaDA;

        public ArtistaFlujo(IArtistaDA artistaDA)
        {
            _artistaDA = artistaDA;
        }

        // Sin reglas de negocio: cada método delega en el DA.
        public Task<IEnumerable<ArtistaResponse>> ObtenerArtistas()
        {
            return _artistaDA.Obtener();
        }

        public Task<ArtistaResponse?> ObtenerArtistaPorId(Guid id)
        {
            return _artistaDA.Obtener(id);
        }

        public Task<Guid> CrearArtista(ArtistaRequest artista)
        {
            return _artistaDA.Agregar(artista);
        }

        public Task<bool> ActualizarArtista(Guid id, ArtistaRequest artista)
        {
            return _artistaDA.Editar(id, artista);
        }

        public Task<bool> EliminarArtista(Guid id)
        {
            return _artistaDA.Eliminar(id);
        }
    }
}
