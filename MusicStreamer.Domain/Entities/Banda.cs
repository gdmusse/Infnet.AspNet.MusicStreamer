namespace MusicStreamer.Domain.Entities;

public class Banda
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; set; }

    public ICollection<Album> Albuns { get; set; } = new List<Album>();
}