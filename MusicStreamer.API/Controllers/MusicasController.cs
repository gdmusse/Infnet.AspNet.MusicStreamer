using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class MusicasController : ControllerBase
{
    private readonly IMusicaService _musicaService;

    public MusicasController(IMusicaService musicaService)
    {
        _musicaService = musicaService;
    }

    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] Guid usuarioId, [FromQuery] string? nome, [FromQuery] bool apenasFavoritas = false)
    {
        var musicas = await _musicaService.BuscarAsync(usuarioId, nome, apenasFavoritas);
        return Ok(musicas);
    }

    [HttpPost("favoritar/{musicaId}")]
    public async Task<IActionResult> Favoritar(Guid musicaId, [FromQuery] Guid usuarioId)
    {
        var sucesso = await _musicaService.MarcarComoFavoritaAsync(usuarioId, musicaId);
        if (!sucesso)
            return NotFound("Usuário ou música não encontrados.");
        return Ok(new { mensagem = "Favorito atualizado com sucesso." });
    }
}
