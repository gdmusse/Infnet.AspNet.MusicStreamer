using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using MusicStreamer.Application.Dtos;
using MusicStreamer.Application.Interfaces;

public class UsuariosController : Controller
{
    private readonly HttpClient _httpClient;

    public UsuariosController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(UsuarioCadastroDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var response = await _httpClient.PostAsJsonAsync("api/usuarios", dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Email já cadastrado.");
            return View(dto);
        }

        TempData["CadastroSucessoMensagem"] = "Seu cadastro foi realizado com sucesso! Faça login para começar."; 
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        var model = new UsuarioLoginViewModel();

        if (TempData["CadastroSucessoMensagem"] != null)
        {
            model.MensagemConfirmacao = TempData["CadastroSucessoMensagem"].ToString();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioLoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var response = await _httpClient.PostAsJsonAsync("api/usuarios/login", dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Credenciais inválidas.");
            return View(dto);
        }

        var usuario = await response.Content.ReadFromJsonAsync<UsuarioDto>();

        HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
        HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

        return RedirectToAction("Index", "Home");
    }
}
