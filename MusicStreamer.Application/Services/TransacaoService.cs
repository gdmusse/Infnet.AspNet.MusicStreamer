using MusicStreamer.Application.Dtos;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Interfaces;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepo;
    private readonly IUsuarioRepository _usuarioRepo;

    public TransacaoService(ITransacaoRepository transacaoRepo, IUsuarioRepository usuarioRepo)
    {
        _transacaoRepo = transacaoRepo;
        _usuarioRepo = usuarioRepo;
    }

    public async Task<TransacaoResponseDto> AutorizarAsync(TransacaoRequestDto dto)
    {
        var usuario = await _usuarioRepo.ObterPorIdAsync(dto.UsuarioId);
        if (usuario == null)
            return new TransacaoResponseDto { Autorizada = false, Mensagem = "Usuário não encontrado" };

        var ultima = await _transacaoRepo.ObterUltimaTransacaoAsync(dto.UsuarioId);

        if (ultima != null && !ultima.Autorizada)
        {
            return new TransacaoResponseDto
            {
                Autorizada = false,
                Mensagem = "Transação recusada: ultima transação não autorizada"
            };
        }

        var transacao = new Transacao
        {
            UsuarioId = dto.UsuarioId,
            Comerciante = dto.Comerciante,
            Valor = dto.Valor,
            Autorizada = true
        };

        await _transacaoRepo.AdicionarAsync(transacao);

        return new TransacaoResponseDto
        {
            Autorizada = true,
            Mensagem = "Transação autorizada. Comerciante e usuário notificados",
            DataHora = transacao.DataHora
        };
    }
}
