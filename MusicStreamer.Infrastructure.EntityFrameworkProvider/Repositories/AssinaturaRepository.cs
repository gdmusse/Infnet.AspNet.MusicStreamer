using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Interfaces;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;

public class AssinaturaRepository : IAssinaturaRepository
{
    private readonly AppDbContext _context;

    public AssinaturaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Assinatura?> ObterPorUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Assinaturas.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
    }

    public async Task CriarOuAtualizarAsync(Assinatura assinatura)
    {
        var existente = await ObterPorUsuarioIdAsync(assinatura.UsuarioId);

        if (existente == null)
        {
            _context.Assinaturas.Add(assinatura);
        }
        else
        {
            existente.Plano = assinatura.Plano;
            existente.DataAssinatura = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
}
