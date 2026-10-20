
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IAlbumDA
    {
        Task<IEnumerable<AlbumResponse>> Obtener(string? artista);

        Task<AlbumResponse?> Obtener(Guid Id);

        Task<Guid> Agregar(AlbumRequest album);

        Task<bool> Editar(Guid Id, AlbumRequest album);

        Task<bool> Eliminar(Guid Id);
    }
}
