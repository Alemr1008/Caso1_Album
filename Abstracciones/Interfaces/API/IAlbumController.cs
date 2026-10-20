
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IAlbumController
    {
        Task<IActionResult> Obtener(string? artista);

        Task<IActionResult> Obtener(Guid Id);

        Task<IActionResult> Agregar(AlbumRequest album);

        Task<IActionResult> Editar(Guid Id, AlbumRequest album);

        Task<IActionResult> Eliminar(Guid Id);
    }
}
