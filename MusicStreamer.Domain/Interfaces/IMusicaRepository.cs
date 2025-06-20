using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Interfaces;

public interface IMusicaRepository
{
    Task<Musica?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Musica>> BuscarPorNomeAsync(string nome);
    Task<IEnumerable<Musica>> ListarAsync();
    Task AdicionarAsync(Musica musica);
}