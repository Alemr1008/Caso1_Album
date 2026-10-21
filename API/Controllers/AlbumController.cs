using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
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
    }
}
