using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;

public class BandaRepository : IBandaRepository
{
    private readonly AppDbContext _context;

    public BandaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Banda>> BuscarAsync(string? nome)
    {
        var query = _context.Bandas.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(b => b.Nome.Contains(nome));

        return await query.OrderBy(b => b.Nome).ToListAsync();
    }

    public async Task<Banda?> ObterPorIdAsync(Guid id)
    {
        return await _context.Bandas.FirstOrDefaultAsync(b => b.Id == id);
    }
}
