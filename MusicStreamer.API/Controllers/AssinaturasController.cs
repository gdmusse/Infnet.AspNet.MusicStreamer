using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Dtos;
using MusicStreamer.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AssinaturasController : ControllerBase
{
    private readonly IAssinaturaService _assinaturaService;

    public AssinaturasController(IAssinaturaService assinaturaService)
    {
        _assinaturaService = assinaturaService;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> ObterPorUsuario(Guid usuarioId)
    {
        var assinatura = await _assinaturaService.ObterPlanoAtualAsync(usuarioId);

        if (assinatura == null)
            return NotFound();

        var dto = new AssinaturaDto
        {
            UsuarioId = assinatura.UsuarioId,
            Plano = assinatura.Plano,
            DataAssinatura = assinatura.DataAssinatura
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> EscolherPlano([FromBody] AssinaturaCadastroDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Plano) || dto.UsuarioId == Guid.Empty)
            return BadRequest(new { mensagem = "Dados inválidos." });

        var sucesso = await _assinaturaService.EscolherPlanoAsync(dto);

        if (!sucesso)
            return StatusCode(500, "Erro ao processar assinatura.");

        return Ok(new { mensagem = "Plano atualizado com sucesso!" });
    }
}
