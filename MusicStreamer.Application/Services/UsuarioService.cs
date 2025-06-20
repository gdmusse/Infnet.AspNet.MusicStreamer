using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Interfaces;
using MusicStreamer.ApplicationLayer.Dtos;

namespace MusicStreamer.ApplicationLayer.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> CadastrarUsuarioAsync(UsuarioCadastroDto dto)
    {
        if (await _usuarioRepository.ExisteEmailAsync(dto.Email))
            return null;

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        return usuario;
    }

    public async Task<Usuario?> LoginAsync(UsuarioLoginDto dto)
    {
        return await _usuarioRepository.ObterPorEmailSenhaAsync(dto.Email, dto.Senha);
    }
}
