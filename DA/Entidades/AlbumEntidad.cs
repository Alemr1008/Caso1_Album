
using System;

namespace DA.Entidades
{
    public class AlbumEntidad
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Genero { get; set; } = string.Empty;

        public int AnioLanzamiento { get; set; }

        public int CantidadCanciones { get; set; }

        public Guid ArtistaId { get; set; }

        public ArtistaEntidad Artista { get; set; } = null!;
    }
}
