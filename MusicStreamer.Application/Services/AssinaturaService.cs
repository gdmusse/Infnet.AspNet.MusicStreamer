namespace MusicStreamer.Application.Dtos;
using MusicStreamer.Application.Interfaces;
using MusicStreamer.Domain.Entities;

public class AssinaturaService : IAssinaturaService
{
    private readonly IAssinaturaRepository _assinaturaRepository;

    public AssinaturaService(IAssinaturaRepository assinaturaRepository)
    {
        _assinaturaRepository = assinaturaRepository;
    }

    public async Task<bool> EscolherPlanoAsync(AssinaturaCadastroDto dto)
    {
        var novaAssinatura = new Assinatura
        {
            UsuarioId = dto.UsuarioId,
            Plano = dto.Plano,
            DataAssinatura = DateTime.Now
        };

        await _assinaturaRepository.CriarOuAtualizarAsync(novaAssinatura);
        return true;
    }

    public async Task<Assinatura?> ObterPlanoAtualAsync(Guid usuarioId)
    {
        return await _assinaturaRepository.ObterPorUsuarioIdAsync(usuarioId);
    }
}
