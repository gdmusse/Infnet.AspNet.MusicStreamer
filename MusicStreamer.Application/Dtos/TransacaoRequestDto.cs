namespace MusicStreamer.Application.Dtos;
public class TransacaoRequestDto
{
    public Guid UsuarioId { get; set; }
    public string Comerciante { get; set; }
    public decimal Valor { get; set; }
}
