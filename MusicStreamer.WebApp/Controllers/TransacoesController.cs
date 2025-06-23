using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Application.Dtos;
using MusicStreamer.WebApp.Models;
using System.Drawing;

public class TransacoesController : Controller
{
    private readonly HttpClient _httpClient;

    public TransacoesController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    [HttpGet]
    public IActionResult Index(string comerciante, decimal valor)
    {
        var model = new TransacaoViewModel
        {
            Comerciante = comerciante,
            Valor = valor
        };

        model.ComerciantesDisponiveis = new List<string> { "Pix", "Cartao Credito", "Boleto" };

        if (TempData["MensagemAssinatura"] != null)
        {
            model.MensagemAssinatura = TempData["MensagemAssinatura"].ToString();
        }

        return View(model);
    }

    [SessionAuthorize]
    [HttpPost]
    public async Task<IActionResult> Nova(TransacaoRequestDto dto)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        Guid.TryParse(usuarioIdStr, out Guid usuarioId);
        dto.UsuarioId = usuarioId;

        var response = await _httpClient.PostAsJsonAsync("api/transacoes", dto);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            ModelState.AddModelError("", erro?["mensagem"] ?? "Erro ao processar transação.");
            return View(dto);
        }

        var resultado = await response.Content.ReadFromJsonAsync<TransacaoResponseDto>();
        TempData["Mensagem"] = resultado!.Mensagem;
        TempData["DataHora"] = resultado.DataHora.ToString("g");

        return RedirectToAction("Confirmacao");
    }

    [HttpGet]
    public IActionResult Confirmacao()
    {
        ViewBag.Mensagem = TempData["Mensagem"];
        ViewBag.DataHora = TempData["DataHora"];
        return View();
    }
}
