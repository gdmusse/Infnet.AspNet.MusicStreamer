using MusicStreamer.Application.DTOs;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Application.Interfaces;

public interface IUsuarioService
{
    Task<Usuario?> LoginAsync(string email, string senha);
    Task<Usuario> RegistrarAsync(UsuarioDto dto);
}
