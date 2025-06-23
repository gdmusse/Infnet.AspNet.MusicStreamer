using MusicStreamer.Application.Dtos;

namespace MusicStreamer.WebApp.Models
{
    public class MusicaBandaViewModel
    {
        public Guid UsuarioId { get; set; }
        public string? NomeBusca { get; set; }
        public bool ApenasFavoritas { get; set; }

        public List<MusicaDto> Musicas { get; set; } = new();
        public List<BandaDto> Bandas { get; set; } = new();
    }
}