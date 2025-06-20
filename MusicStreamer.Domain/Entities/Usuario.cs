namespace MusicStreamer.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }

    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    public ICollection<Musica> MusicasFavoritas { get; set; } = new List<Musica>();
    public ICollection<Banda> BandasFavoritas { get; set; } = new List<Banda>();

    public Assinatura? Assinatura { get; set; }
}