namespace MusicStreamer.Application.Dtos;

public class AssinaturaCadastroDto
{
    public Guid UsuarioId { get; set; }
    public string Plano { get; set; } = null!;
}
