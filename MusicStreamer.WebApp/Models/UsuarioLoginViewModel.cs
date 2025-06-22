namespace MusicStreamer.Application.Dtos
{
    public class UsuarioLoginViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? MensagemConfirmacao { get; set; }
    }
}
