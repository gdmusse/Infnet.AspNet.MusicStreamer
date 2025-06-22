namespace MusicStreamer.Application.Dtos;

public class AssinaturaDto
{
    public Guid UsuarioId { get; set; }
    public string Plano { get; set; } = null!;
    public DateTime DataAssinatura { get; set; }
}
