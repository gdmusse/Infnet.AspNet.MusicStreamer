using Microsoft.AspNetCore.Mvc;
using MusicStreamer.ApplicationLayer.Dtos;
using MusicStreamer.ApplicationLayer.Services;

namespace MusicStreamer.API.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDto dto)
    {
        var usuario = await _usuarioService.CadastrarUsuarioAsync(dto);
        if (usuario == null)
            return BadRequest("Email já cadastrado.");

        return Ok(new { usuario.Id, usuario.Nome, usuario.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
    {
        var usuario = await _usuarioService.LoginAsync(dto);
        if (usuario == null)
            return Unauthorized("Credenciais inválidas.");

        return Ok(new { usuario.Id, usuario.Nome, usuario.Email });
    }
}
