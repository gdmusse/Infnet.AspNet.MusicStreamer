using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Dtos;
using MusicStreamer.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> AutorizarTransacao([FromBody] TransacaoRequestDto dto)
    {
        if (dto == null || dto.UsuarioId == Guid.Empty || string.IsNullOrWhiteSpace(dto.Comerciante) || dto.Valor <= 0)
            return BadRequest(new { mensagem = "Dados inválidos." });

        var resultado = await _transacaoService.AutorizarAsync(dto);

        if (!resultado.Autorizada)
            return BadRequest(new { mensagem = resultado.Mensagem });

        return Ok(resultado);
    }
}
