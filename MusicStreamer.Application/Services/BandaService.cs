using MusicStreamer.Application.Dtos;
using MusicStreamer.Domain.Interfaces;

public class BandaService : IBandaService
{
    private readonly IBandaRepository _bandaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public BandaService(IBandaRepository bandaRepository, IUsuarioRepository usuarioRepository)
    {
        _bandaRepository = bandaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<BandaDto>> BuscarAsync(Guid usuarioId, string? nome, bool apenasFavoritas)
    {
        var usuario = await _usuarioRepository.ObterPorIdComFavoritosAsync(usuarioId);
        var bandas = await _bandaRepository.BuscarAsync(nome);

        return bandas
            .Where(b => !apenasFavoritas || usuario.BandasFavoritas.Any(f => f.Id == b.Id))
            .Select(b => new BandaDto
            {
                Id = b.Id,
                Nome = b.Nome,
                Favorita = usuario.BandasFavoritas.Any(f => f.Id == b.Id)
            });
    }

    public async Task<bool> MarcarComoFavoritaAsync(Guid usuarioId, Guid bandaId)
    {
        var usuario = await _usuarioRepository.ObterPorIdComFavoritosAsync(usuarioId);
        var banda = await _bandaRepository.ObterPorIdAsync(bandaId);

        if (usuario == null || banda == null)
            return false;

        if (!usuario.BandasFavoritas.Any(b => b.Id == bandaId))
            usuario.BandasFavoritas.Add(banda);
        else
            usuario.BandasFavoritas.Remove(banda);

        await _usuarioRepository.SaveChangesAsync();
        return true;
    }
}
