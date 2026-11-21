using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Flujo;
using Moq;
using Xunit;

namespace Pruebas
{
    public class AlbumFlujoPruebas
    {
        private readonly Mock<IAlbumDA> _albumDA = new();
        private readonly Mock<IArtistaDA> _artistaDA = new();
        private readonly AlbumFlujo _flujo;

        public AlbumFlujoPruebas()
        {
            _flujo = new AlbumFlujo(_albumDA.Object, _artistaDA.Object);
        }

        [Fact]
        public async Task Agregar_ArtistaNoExiste_RetornaGuidEmptyYNoGuarda()
        {
            // Arrange
            var request = new AlbumRequest { Titulo = "Prueba", ArtistaId = Guid.NewGuid() };
            _artistaDA.Setup(d => d.Obtener(request.ArtistaId))
                      .ReturnsAsync((ArtistaResponse?)null);

            // Act
            var resultado = await _flujo.Agregar(request);

            // Assert
            Assert.Equal(Guid.Empty, resultado);
            _albumDA.Verify(d => d.Agregar(It.IsAny<AlbumRequest>()), Times.Never);
        }

        [Fact]
        public async Task Agregar_ArtistaExiste_GuardaYRetornaId()
        {
            // Arrange
            var request = new AlbumRequest { Titulo = "Prueba", ArtistaId = Guid.NewGuid() };
            var idEsperado = Guid.NewGuid();
            _artistaDA.Setup(d => d.Obtener(request.ArtistaId))
                      .ReturnsAsync(new ArtistaResponse { Id = request.ArtistaId, Nombre = "Artista" });
            _albumDA.Setup(d => d.Agregar(request)).ReturnsAsync(idEsperado);

            // Act
            var resultado = await _flujo.Agregar(request);

            // Assert
            Assert.Equal(idEsperado, resultado);
            _albumDA.Verify(d => d.Agregar(request), Times.Once);
        }

        [Fact]
        public async Task ExportarCsv_GeneraEncabezadoYUnaFilaPorAlbum()
        {
            // Arrange
            var lista = new List<AlbumResponse>
            {
                new() { Id = Guid.NewGuid(), Titulo = "Thriller", Artista = "Michael Jackson", Genero = "Pop", AnioLanzamiento = 1982, CantidadCanciones = 9 },
                new() { Id = Guid.NewGuid(), Titulo = "Abbey Road", Artista = "The Beatles", Genero = "Rock", AnioLanzamiento = 1969, CantidadCanciones = 17 }
            };
            _albumDA.Setup(d => d.Obtener(It.IsAny<string?>())).ReturnsAsync(lista);

            // Act
            var resultado = await _flujo.ExportarCsv();

            // Assert
            var lineas = resultado.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(3, lineas.Length);
            Assert.StartsWith("Id,Titulo,Artista", lineas[0]);
        }
    }
}
