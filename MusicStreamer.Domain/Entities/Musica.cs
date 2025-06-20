namespace MusicStreamer.Domain.Entities;

public class Musica
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; set; }
    public TimeSpan Duracao { get; set; }

    public Guid AlbumId { get; set; }
    public Album Album { get; set; }
}