using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task AdicionarAsync(Transacao transacao);
    Task<Transacao?> ObterUltimaTransacaoAsync(Guid usuarioId);
}