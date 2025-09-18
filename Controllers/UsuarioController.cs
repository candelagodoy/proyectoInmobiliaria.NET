using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class UsuarioController : Controller
{

    private RepositorioUsuario repo;

    public UsuarioController()
    {
        repo = new RepositorioUsuario();
    }
    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Registrar()
    {
        Usuario usuario = new Usuario();
        usuario.idUsuario = 0;
        usuario.nombre = "Ejemplo";
        usuario.apellido = "Ejemplo";
        usuario.email = "nose@nose.com";
        usuario.clave = "1234";
        usuario.avatar = "https://www.w3schools.com/howto/img_avatar.png";
        usuario.rol = 1;
        repo.Alta(usuario);
           return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Registrar(Usuario usuario)
    {
        repo.Alta(usuario);
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Eliminar(int id)
    {
        repo.Baja(id);
           return RedirectToAction(nameof(Index));
    }

    public IActionResult Editar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Editar(Usuario usuario)
    {
        repo.Modificacion(usuario);
        return View();
    }

}
