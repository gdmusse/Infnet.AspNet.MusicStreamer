namespace MusicStreamer.Application.Dtos;

public class TransacaoResponseDto
{
    public bool Autorizada { get; set; }
    public string? Mensagem { get; set; }
    public DateTime DataHora { get; set; }
}
