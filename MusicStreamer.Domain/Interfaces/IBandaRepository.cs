using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Interfaces;

public interface IBandaRepository
{
    Task<Banda?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Banda>> BuscarPorNomeAsync(string nome);
    Task<IEnumerable<Banda>> ListarAsync();
    Task AdicionarAsync(Banda banda);
}