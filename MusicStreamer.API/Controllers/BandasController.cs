using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BandasController : ControllerBase
{
    private readonly IBandaService _bandaService;

    public BandasController(IBandaService bandaService)
    {
        _bandaService = bandaService;
    }

    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] Guid usuarioId, [FromQuery] string? nome, [FromQuery] bool apenasFavoritas = false)
    {
        var bandas = await _bandaService.BuscarAsync(usuarioId, nome, apenasFavoritas);
        return Ok(bandas);
    }

    [HttpPost("favoritar/{bandaId}")]
    public async Task<IActionResult> Favoritar(Guid bandaId, [FromQuery] Guid usuarioId)
    {
        var sucesso = await _bandaService.MarcarComoFavoritaAsync(usuarioId, bandaId);
        if (!sucesso)
            return NotFound("Usuário ou banda não encontrados.");
        return Ok(new { mensagem = "Favorito atualizado com sucesso." });
    }
}
