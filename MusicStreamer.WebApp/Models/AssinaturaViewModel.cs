namespace MusicStreamer.Application.Dtos
{
    public class AssinaturaViewModel
    {
        public AssinaturaDto? PlanoAtual { get; set; } 
        public List<string> PlanosDisponiveis { get; set; } = new List<string>(); 
        public string? MensagemConfirmacao { get; set; } 
    }
}