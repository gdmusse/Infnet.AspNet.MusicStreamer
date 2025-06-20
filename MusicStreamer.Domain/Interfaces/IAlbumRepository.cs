using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Interfaces;

public interface IAlbumRepository
{
    Task<Album?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Album>> ListarPorBandaAsync(Guid bandaId);
    Task AdicionarAsync(Album album);
}
