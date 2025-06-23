using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Dtos;
using MusicStreamer.WebApp.Models;
using System.Net.Http;

public class MusicasController : Controller
{
    private readonly HttpClient _httpClient;

    public MusicasController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    [SessionAuthorize]
    public async Task<IActionResult> Index(string? nome, bool apenasFavoritas = false)
    {
        var usuarioId = ObterUsuarioLogado();
        if (usuarioId == null)
            return RedirectToAction("Login", "Usuario");

        var musicaResponse = await _httpClient.GetAsync($"/api/musicas?usuarioId={usuarioId}&nome={nome}&apenasFavoritas={apenasFavoritas}");
        var bandaResponse = await _httpClient.GetAsync($"/api/bandas?usuarioId={usuarioId}&nome={nome}&apenasFavoritas={apenasFavoritas}");

        var musicas = await musicaResponse.Content.ReadFromJsonAsync<List<MusicaDto>>() ?? new();
        var bandas = await bandaResponse.Content.ReadFromJsonAsync<List<BandaDto>>() ?? new();

        var vm = new MusicaBandaViewModel
        {
            UsuarioId = usuarioId.Value,
            NomeBusca = nome,
            ApenasFavoritas = apenasFavoritas,
            Musicas = musicas,
            Bandas = bandas
        };

        return View(vm);
    }

    [SessionAuthorize]
    [HttpPost]
    public async Task<IActionResult> FavoritarMusica(Guid musicaId)
    {
        var usuarioId = ObterUsuarioLogado();

        await _httpClient.PostAsync($"/api/musicas/favoritar/{musicaId}?usuarioId={usuarioId}", null);
        return RedirectToAction("Index");
    }

    [SessionAuthorize]
    [HttpPost]
    public async Task<IActionResult> FavoritarBanda(Guid bandaId)
    {
        var usuarioId = ObterUsuarioLogado();

        await _httpClient.PostAsync($"/api/bandas/favoritar/{bandaId}?usuarioId={usuarioId}", null);
        return RedirectToAction("Index");
    }

    private Guid? ObterUsuarioLogado()
    {
        var idStr = HttpContext.Session.GetString("UsuarioId");
        return Guid.TryParse(idStr, out var id) ? id : null;
    }
}
