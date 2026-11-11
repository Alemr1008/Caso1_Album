using System.Text;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class AlbumFlujo : IAlbumFlujo
    {
        private readonly IAlbumDA _albumDA;
        private readonly IArtistaDA _artistaDA;

        public AlbumFlujo(IAlbumDA albumDA, IArtistaDA artistaDA)
        {
            _albumDA = albumDA;
            _artistaDA = artistaDA;
        }

        public Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            return _albumDA.Obtener(artista);
        }

        public Task<AlbumResponse?> Obtener(Guid Id)
        {
            return _albumDA.Obtener(Id);
        }

        // Regla de negocio: no se guarda un álbum de un artista que no existe.
        public async Task<Guid> Agregar(AlbumRequest album)
        {
            if (await _artistaDA.Obtener(album.ArtistaId) == null)
                return Guid.Empty;

            return await _albumDA.Agregar(album);
        }

        public async Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            if (await _artistaDA.Obtener(album.ArtistaId) == null)
                return false;

            return await _albumDA.Editar(Id, album);
        }

        public Task<bool> Eliminar(Guid Id)
        {
            return _albumDA.Eliminar(Id);
        }

        public async Task<string> ExportarCsv()
        {
            var albumes = await _albumDA.Obtener(null);

            var csv = new StringBuilder();
            csv.AppendLine("Id,Titulo,Artista,Genero,AnioLanzamiento,CantidadCanciones");

            foreach (var a in albumes)
            {
                csv.AppendLine($"{a.Id},{Escapar(a.Titulo)},{Escapar(a.Artista)},{Escapar(a.Genero)},{a.AnioLanzamiento},{a.CantidadCanciones}");
            }

            return csv.ToString();
        }

        // Protege los valores que contienen comas o comillas.
        private static string Escapar(string valor)
        {
            if (valor.Contains(',') || valor.Contains('"'))
                return "\"" + valor.Replace("\"", "\"\"") + "\"";

            return valor;
        }
    }
}
