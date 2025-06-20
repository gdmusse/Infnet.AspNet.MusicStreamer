using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Interfaces;

public interface IPlaylistRepository
{
    Task<Playlist?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Playlist>> ListarPorUsuarioAsync(Guid usuarioId);
    Task AdicionarAsync(Playlist playlist);
    Task AtualizarAsync(Playlist playlist);
}