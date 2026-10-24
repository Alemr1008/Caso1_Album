using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class AlbumFlujo : IAlbumFlujo
    {
        private readonly IAlbumDA _albumDA;

        public AlbumFlujo(IAlbumDA albumDA)
        {
            _albumDA = albumDA;
        }

        // Sin reglas de negocio (cada método delega en el DA.)
        public Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            return _albumDA.Obtener(artista);
        }

        public Task<AlbumResponse?> Obtener(Guid Id)
        {
            return _albumDA.Obtener(Id);
        }

        public Task<Guid> Agregar(AlbumRequest album)
        {
            return _albumDA.Agregar(album);
        }

        public Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            return _albumDA.Editar(Id, album);
        }

        public Task<bool> Eliminar(Guid Id)
        {
            return _albumDA.Eliminar(Id);
        }
    }
}
