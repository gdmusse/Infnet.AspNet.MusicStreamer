using Microsoft.AspNetCore.Mvc;
using MusicStreamer.ApplicationLayer.Dtos;
using MusicStreamer.ApplicationLayer.Services;

public class UsuariosController : Controller
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(UsuarioCadastroDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var usuario = await _usuarioService.CadastrarUsuarioAsync(dto);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Email já cadastrado.");
            return View(dto);
        }

        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioLoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var usuario = await _usuarioService.LoginAsync(dto);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Credenciais inválidas.");
            return View(dto);
        }

        HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
        HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

        return RedirectToAction("Index", "Home");
    }
}
