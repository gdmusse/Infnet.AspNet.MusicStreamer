using MusicStreamer.Application.Dtos;

public interface IBandaService
{
    Task<IEnumerable<BandaDto>> BuscarAsync(Guid usuarioId, string? nome, bool apenasFavoritas);
    Task<bool> MarcarComoFavoritaAsync(Guid usuarioId, Guid bandaId);
}
