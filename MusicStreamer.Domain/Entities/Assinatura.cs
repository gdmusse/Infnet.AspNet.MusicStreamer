namespace MusicStreamer.Domain.Entities;

public class Assinatura
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Plano { get; set; } 
    public DateTime DataAssinatura { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}