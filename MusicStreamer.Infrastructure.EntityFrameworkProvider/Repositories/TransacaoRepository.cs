using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Interfaces;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;
using Microsoft.EntityFrameworkCore;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task<Transacao?> ObterUltimaTransacaoAsync(Guid usuarioId)
    {
        return await _context.Transacoes
            .Where(t => t.UsuarioId == usuarioId)
            .OrderByDescending(t => t.DataHora)
            .FirstOrDefaultAsync();
    }
}
