namespace MusicStreamer.Domain.Interfaces;
using MusicStreamer.Domain.Entities;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterPorEmailSenhaAsync(string email, string senha);
    Task<bool> ExisteEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
}
