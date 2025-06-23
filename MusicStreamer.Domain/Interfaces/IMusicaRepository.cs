public interface IMusicaRepository
{
    Task<IEnumerable<Musica>> BuscarComBandaAsync(string? nome);
    Task<Musica?> ObterPorIdAsync(Guid id);
}
