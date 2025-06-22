using MusicStreamer.Domain.Entities;

public interface IAssinaturaRepository
{
    Task<Assinatura?> ObterPorUsuarioIdAsync(Guid usuarioId);
    Task CriarOuAtualizarAsync(Assinatura assinatura);
}
