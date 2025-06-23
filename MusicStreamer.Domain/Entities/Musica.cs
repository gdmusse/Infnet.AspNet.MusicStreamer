using MusicStreamer.Domain.Entities;

public class Musica
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; set; }

    public Guid BandaId { get; set; }
    public Banda Banda { get; set; }

    public ICollection<Usuario> FavoritadaPorUsuarios { get; set; } = new List<Usuario>();
}