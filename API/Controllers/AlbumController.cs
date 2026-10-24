using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase, IAlbumController
    {
        private readonly IAlbumFlujo _albumFlujo;

        public AlbumController(IAlbumFlujo albumFlujo)
        {
            _albumFlujo = albumFlujo;
        }

        // GET api/Album?artista=beatles
        [HttpGet]
        public async Task<IActionResult> Obtener([FromQuery] string? artista)
        {
            var albumes = await _albumFlujo.Obtener(artista);
            return Ok(albumes); // 200
        }

        // GET api/Album/{Id}
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var album = await _albumFlujo.Obtener(Id);
            if (album == null)
                return NotFound(); // 404
            return Ok(album); // 200
        }

        // POST api/Album
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] AlbumRequest album)
        {
            var id = await _albumFlujo.Agregar(album);
            var albumCreado = await _albumFlujo.Obtener(id);
            return CreatedAtAction(nameof(Obtener), new { Id = id }, albumCreado); // 201 + Location
        }

        // PUT api/Album/{Id}
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] AlbumRequest album)
        {
            var editado = await _albumFlujo.Editar(Id, album);
            if (!editado)
                return NotFound(); // 404
            return NoContent(); // 204
        }

        // DELETE api/Album/{Id}
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            var eliminado = await _albumFlujo.Eliminar(Id);
            if (!eliminado)
                return NotFound(); // 404
            return NoContent(); // 204
        }
    }
}
