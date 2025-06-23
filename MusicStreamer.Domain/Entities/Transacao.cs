namespace MusicStreamer.Domain.Entities;

public class Transacao
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public string Comerciante { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;

    public bool Autorizada { get; set; }
    public string? MotivoNegacao { get; set; }
}