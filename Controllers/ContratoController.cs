using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class ContratoController : Controller
{
    private RepositorioContrato repo;
    private RepositorioInquilino repoInquilino;
    private RepositorioInmueble repoInmueble;
    private RepositorioUsuario repoUsuario;

    public ContratoController()
    {
        repo = new RepositorioContrato();
        repoInquilino = new RepositorioInquilino();
        repoInmueble = new RepositorioInmueble();
        repoUsuario = new RepositorioUsuario();
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Create()
    {
        ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
        ViewBag.Usuarios = repoUsuario.ObtenerTodos();
        ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
        return View();
    }

    [HttpPost]
    public IActionResult Create(Contrato contrato)
    {
        repo.Alta(contrato);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var contrato = repo.ObtenerPorId(id);
        ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
        ViewBag.Usuarios = repoUsuario.ObtenerTodos();
        ViewBag.InquilinoSelected = contrato.idInquilino;
        ViewBag.InmuebleSelected = contrato.idInmueble;
        ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
        return View(contrato);
    }

    [HttpPost]
    public IActionResult Edit(Contrato contrato)
    {

        repo.Modificacion(contrato);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        int usuarioLogeadoId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
        repo.Baja(id, usuarioLogeadoId);
        return RedirectToAction(nameof(Index));

        /*var con = repo.ObtenerPorId(id);
        int idUsuarioLogin = int.Parse(User.FindFirst("Id")?.Value);
        con.idUsuarioBaja = idUsuarioLogin;
        repo.Modificacion(con);
        repo.Baja(id);
        return RedirectToAction(nameof(Index));*/
    }

    public IActionResult Detalles(int id)

    {
        var contrato = repo.ObtenerPorId(id);
        var inquilino = repoInquilino.ObtenerPorId(contrato.idInquilino);
        var inmueble = repoInmueble.ObtenerPorId(contrato.idInmueble);
        var usuario = repoUsuario.ObtenerPorId(contrato.idUsuarioAlta);
        ViewBag.PropietarioNombre = inquilino.nombre + " " + inquilino.apellido;
        ViewBag.Inmueble = inmueble.direccion;
        ViewBag.Usuario = usuario.ToString();

        if (contrato == null)
        {
            return NotFound();
        }

        return View(contrato);
    }
    
    public IActionResult Anulados()
    {
        var lista = repo.ObtenerContratosTerminados();
        return View("Anulados", lista);
    }
}
