namespace MusicStreamer.Domain.Entities;

public class Album
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; set; }

    public Guid BandaId { get; set; }
    public Banda Banda { get; set; }

    public ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}