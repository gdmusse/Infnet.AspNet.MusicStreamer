using MusicStreamer.Application.Dtos;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Application.Interfaces;

public interface IAssinaturaService
{
    Task<bool> EscolherPlanoAsync(AssinaturaCadastroDto dto);
    Task<Assinatura?> ObterPlanoAtualAsync(Guid usuarioId);
}
