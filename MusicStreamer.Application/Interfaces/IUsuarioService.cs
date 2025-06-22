using MusicStreamer.Application.Dtos;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Application.Interfaces;

public interface IUsuarioService
{
    Task<Usuario?> CadastrarUsuarioAsync(UsuarioCadastroDto dto);
    Task<Usuario?> LoginAsync(UsuarioLoginDto dto);
}
