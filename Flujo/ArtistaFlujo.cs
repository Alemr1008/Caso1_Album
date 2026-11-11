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
        public Task<IEnumerable<ArtistaResponse>> Obtener()
        {
            return _artistaDA.Obtener();
        }

        public Task<ArtistaResponse?> Obtener(Guid Id)
        {
            return _artistaDA.Obtener(Id);
        }

        public Task<Guid> Agregar(ArtistaRequest artista)
        {
            return _artistaDA.Agregar(artista);
        }
    }
}
