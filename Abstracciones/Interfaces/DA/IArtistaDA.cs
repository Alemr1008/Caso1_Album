using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IArtistaDA
    {
        Task<IEnumerable<ArtistaResponse>> Obtener();

        Task<ArtistaResponse?> Obtener(Guid Id);

        Task<Guid> Agregar(ArtistaRequest artista);

        Task<bool> Editar(Guid Id, ArtistaRequest artista);

        Task<bool> Eliminar(Guid Id);
    }
}
