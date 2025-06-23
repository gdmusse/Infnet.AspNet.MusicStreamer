namespace MusicStreamer.Domain.Interfaces;
using MusicStreamer.Domain.Entities;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterPorEmailSenhaAsync(string email, string senha);
    Task<bool> ExisteEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
    Task<Usuario?> ObterPorIdComFavoritosAsync(Guid id);
    Task SaveChangesAsync();
}
