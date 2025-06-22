using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using MusicStreamer.Application.Dtos;

public class AssinaturasController : Controller
{
    private readonly HttpClient _httpClient;

    public AssinaturasController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    [SessionAuthorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new AssinaturaViewModel();
        model.PlanosDisponiveis = new List<string> { "Free", "Premium", "Família" };

        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        Guid.TryParse(usuarioIdStr, out Guid usuarioId);

        if (usuarioId != Guid.Empty)
        {
            var response = await _httpClient.GetAsync($"api/assinaturas/{usuarioIdStr}");

            if (response.IsSuccessStatusCode)
            {
                model.PlanoAtual = await response.Content.ReadFromJsonAsync<AssinaturaDto>();
            }
            else
            {
            }
        }

        if (TempData["MensagemAssinatura"] != null)
        {
            model.MensagemConfirmacao = TempData["MensagemAssinatura"].ToString();
        }

        return View(model);
    }

    [SessionAuthorize]
    [HttpPost]
    public async Task<IActionResult> Escolher(string planoSelecionado)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        Guid.TryParse(usuarioIdStr, out Guid usuarioId);

        var dto = new AssinaturaCadastroDto
        {
            UsuarioId = usuarioId,
            Plano = planoSelecionado
        };

        var response = await _httpClient.PostAsJsonAsync("api/assinaturas", dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Erro ao atualizar plano. Por favor, tente novamente.");
            TempData["Mensagem"] = "Erro ao atualizar plano. Por favor, tente novamente.";

            return RedirectToAction("Index"); 
        }

        TempData["MensagemAssinatura"] = "Plano escolhido com sucesso!";
        return RedirectToAction("Index");
    }
}