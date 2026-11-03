
using System;
using System.Collections.Generic;

namespace DA.Entidades
{
    public class ArtistaEntidad
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Pais { get; set; } = string.Empty;

        public ICollection<AlbumEntidad> Albumes { get; set; }
            = new List<AlbumEntidad>();
    }
}
