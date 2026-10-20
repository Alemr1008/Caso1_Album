
using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;

namespace DA
{
    public class AlbumDA : IAlbumDA
    {
        // Lista estática que conserva los álbumes
        // mientras la aplicación esté ejecutándose.
        private static readonly List<AlbumResponse> albumes = new()
        {
            new AlbumResponse
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Titulo = "Thriller",
                Artista = "Michael Jackson",
                Genero = "Pop",
                AnioLanzamiento = 1982,
                CantidadCanciones = 9
            },

            new AlbumResponse
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Titulo = "Abbey Road",
                Artista = "The Beatles",
                Genero = "Rock",
                AnioLanzamiento = 1969,
                CantidadCanciones = 17
            },

            new AlbumResponse
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Titulo = "Un Verano Sin Ti",
                Artista = "Bad Bunny",
                Genero = "Reggaetón",
                AnioLanzamiento = 2022,
                CantidadCanciones = 23
            }
        };

        // GET: Obtener todos los álbumes o filtrar por artista.
        public Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            IEnumerable<AlbumResponse> resultado = albumes;

            if (!string.IsNullOrWhiteSpace(artista))
            {
                resultado = albumes.Where(a =>
                    a.Artista.Contains(
                        artista,
                        StringComparison.OrdinalIgnoreCase
                    )
                );
            }

            return Task.FromResult(resultado);
        }

        // GET: Obtener un álbum por su identificador.
        public Task<AlbumResponse?> Obtener(Guid Id)
        {
            AlbumResponse? album = albumes.FirstOrDefault(
                a => a.Id == Id
            );

            return Task.FromResult(album);
        }

        //Agregar (POST).
        public Task<Guid> Agregar(AlbumRequest album)
        {
            // Crear el nuevo álbum con un identificador único.
            AlbumResponse nuevoAlbum = new AlbumResponse
            {
                Id = Guid.NewGuid(),
                Titulo = album.Titulo,
                Artista = album.Artista,
                Genero = album.Genero,
                AnioLanzamiento = album.AnioLanzamiento,
                CantidadCanciones = album.CantidadCanciones
            };

            // Agregar el álbum a la lista en memoria.
            albumes.Add(nuevoAlbum);

            // Devolver el identificador generado.
            return Task.FromResult(nuevoAlbum.Id);
        }

        //Editar (PUT).

        public Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            // Buscar el álbum que se desea modificar.
            AlbumResponse? albumExistente =
                albumes.FirstOrDefault(a => a.Id == Id);

            // Si no existe, devolver false.
            if (albumExistente == null)
            {
                return Task.FromResult(false);
            }

            // Actualizar los datos del álbum.
            albumExistente.Titulo = album.Titulo;
            albumExistente.Artista = album.Artista;
            albumExistente.Genero = album.Genero;
            albumExistente.AnioLanzamiento = album.AnioLanzamiento;
            albumExistente.CantidadCanciones = album.CantidadCanciones;

            return Task.FromResult(true);
        }

        //Eliminar (DELETE).
        public Task<bool> Eliminar(Guid Id)
        {
            // Buscar el álbum que se desea eliminar.
            AlbumResponse? albumExistente =
                albumes.FirstOrDefault(a => a.Id == Id);

            // Si no existe, devolver false.
            if (albumExistente == null)
            {
                return Task.FromResult(false);
            }

            // Eliminar el álbum de la lista.
            albumes.Remove(albumExistente);

            return Task.FromResult(true);
        }

    }
}
