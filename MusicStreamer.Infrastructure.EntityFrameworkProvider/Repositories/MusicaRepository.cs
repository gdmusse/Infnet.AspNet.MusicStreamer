using Microsoft.EntityFrameworkCore;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;

public class MusicaRepository : IMusicaRepository
{
    private readonly AppDbContext _context;

    public MusicaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Musica>> BuscarComBandaAsync(string? nome)
    {
        var query = _context.Musicas
            .Include(m => m.Banda)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(m => m.Titulo.Contains(nome) || m.Banda.Nome.Contains(nome));

        return await query.OrderBy(m => m.Banda.Nome).ThenBy(m => m.Titulo).ToListAsync();
    }

    public async Task<Musica?> ObterPorIdAsync(Guid id)
    {
        return await _context.Musicas
            .Include(m => m.Banda)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}
