using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IArtistaFlujo
    {
        Task<IEnumerable<ArtistaResponse>> Obtener();

        Task<ArtistaResponse?> Obtener(Guid Id);

        Task<Guid> Agregar(ArtistaRequest artista);
    }
}
