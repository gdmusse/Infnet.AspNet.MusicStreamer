using MusicStreamer.Application.Dtos;
using MusicStreamer.Domain.Interfaces;

public class MusicaService : IMusicaService
{
    private readonly IMusicaRepository _musicaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public MusicaService(IMusicaRepository musicaRepository, IUsuarioRepository usuarioRepository)
    {
        _musicaRepository = musicaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<MusicaDto>> BuscarAsync(Guid usuarioId, string? nome, bool apenasFavoritas)
    {
        var usuario = await _usuarioRepository.ObterPorIdComFavoritosAsync(usuarioId);
        var musicas = await _musicaRepository.BuscarComBandaAsync(nome);

        return musicas
            .Where(m => !apenasFavoritas || usuario.MusicasFavoritas.Any(f => f.Id == m.Id))
            .Select(m => new MusicaDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                BandaNome = m.Banda.Nome,
                Favorita = usuario.MusicasFavoritas.Any(f => f.Id == m.Id)
            });
    }

    public async Task<bool> MarcarComoFavoritaAsync(Guid usuarioId, Guid musicaId)
    {
        var usuario = await _usuarioRepository.ObterPorIdComFavoritosAsync(usuarioId);
        var musica = await _musicaRepository.ObterPorIdAsync(musicaId);

        if (usuario == null || musica == null)
            return false;

        if (!usuario.MusicasFavoritas.Any(m => m.Id == musicaId))
            usuario.MusicasFavoritas.Add(musica);
        else
            usuario.MusicasFavoritas.Remove(musica);

        await _usuarioRepository.SaveChangesAsync();
        return true;
    }
}
