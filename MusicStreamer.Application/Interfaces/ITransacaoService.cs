using MusicStreamer.Application.Dtos;

public interface ITransacaoService
{
    Task<TransacaoResponseDto> AutorizarAsync(TransacaoRequestDto dto);
}
