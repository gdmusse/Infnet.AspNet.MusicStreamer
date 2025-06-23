using MusicStreamer.Domain.Entities;

public interface IBandaRepository
{
    Task<IEnumerable<Banda>> BuscarAsync(string? nome);
    Task<Banda?> ObterPorIdAsync(Guid id);
}
