using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using DA.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DA
{
    public class ArtistaDA : IArtistaDA
    {
        private readonly VinilosDbContext _contexto;

        public ArtistaDA(VinilosDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: todos los artistas ordenados por nombre.
        public async Task<IEnumerable<ArtistaResponse>> Obtener()
        {
            var artistas = await _contexto.Artistas
                .AsNoTracking()
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return artistas.Select(AResponse);
        }

        // GET: un artista por su Id, o null si no existe.
        public async Task<ArtistaResponse?> Obtener(Guid Id)
        {
            var artista = await _contexto.Artistas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == Id);

            return artista == null ? null : AResponse(artista);
        }

        // POST: crea el artista y devuelve el Id generado.
        public async Task<Guid> Agregar(ArtistaRequest artista)
        {
            var entidad = new ArtistaEntidad
            {
                Id = Guid.NewGuid(),
                Nombre = artista.Nombre,
                Pais = artista.Pais
            };

            _contexto.Artistas.Add(entidad);
            await _contexto.SaveChangesAsync();

            return entidad.Id;
        }

        private static ArtistaResponse AResponse(ArtistaEntidad a) => new ArtistaResponse
        {
            Id = a.Id,
            Nombre = a.Nombre,
            Pais = a.Pais
        };
    }
}
