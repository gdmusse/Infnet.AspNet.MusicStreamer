namespace MusicStreamer.Domain.Entities;

public class Banda
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public ICollection<Musica> Musicas { get; set; } = new List<Musica>();
    public ICollection<Usuario> FavoritadaPorUsuarios { get; set; } = new List<Usuario>();
}