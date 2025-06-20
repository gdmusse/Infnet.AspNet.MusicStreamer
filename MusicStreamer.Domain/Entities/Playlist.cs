namespace MusicStreamer.Domain.Entities;

public class Playlist
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}