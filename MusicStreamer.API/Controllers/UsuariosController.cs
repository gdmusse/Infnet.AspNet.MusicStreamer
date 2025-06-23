using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Interfaces;
using MusicStreamer.Application.Dtos;

namespace MusicStreamer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDto dto)
    {
        var usuario = await _usuarioService.CadastrarUsuarioAsync(dto);
        if (usuario == null)
            return BadRequest(new { mensagem =  "Email j� cadastrado." });

        return Ok(new { usuario.Id, usuario.Nome, usuario.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
    {
        var usuario = await _usuarioService.LoginAsync(dto);
        if (usuario == null)
            return Unauthorized(new { mensagem = "Credenciais inv�lidas." });

        return Ok(new { usuario.Id, usuario.Nome, usuario.Email });
    }
}
