namespace MusicStreamer.WebApp.Models

{
    public class TransacaoViewModel
    {
        public Guid UsuarioId { get; set; }
        public string Comerciante { get; set; }
        public decimal Valor { get; set; }
        public string MensagemAssinatura { get; set; }
        public List<string> ComerciantesDisponiveis { get; set; } = new List<string>();
    }
}
