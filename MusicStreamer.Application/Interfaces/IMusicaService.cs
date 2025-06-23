using MusicStreamer.Application.Dtos;

public interface IMusicaService
{
    Task<IEnumerable<MusicaDto>> BuscarAsync(Guid usuarioId, string? nome, bool apenasFavoritas);
    Task<bool> MarcarComoFavoritaAsync(Guid usuarioId, Guid musicaId);
}
