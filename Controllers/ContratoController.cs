using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

[Authorize]
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
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contrato contrato)
    {

        if (!ModelState.IsValid) return View(contrato);
        if (contrato.fechaDesde > contrato.fechaHasta)
        {
            ModelState.AddModelError("", "La fecha de inicio no puede ser posterior a la de fin.");
            ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
            ViewBag.Usuarios = repoUsuario.ObtenerTodos();
            ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
            return View(contrato);
        }

        if (repo.ExisteSuperposicion(contrato.idInmueble, contrato.fechaDesde, contrato.fechaHasta, null))
        {
            ModelState.AddModelError("", "Se superpone con otro contrato de este inmueble.");
            ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
            ViewBag.Usuarios = repoUsuario.ObtenerTodos();
            ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
            return View(contrato);
        }

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

        if (!ModelState.IsValid) return View(contrato);
        if (contrato.fechaDesde > contrato.fechaHasta)
        {
            ModelState.AddModelError("", "La fecha de inicio no puede ser posterior a la de fin.");
            ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
            ViewBag.Usuarios = repoUsuario.ObtenerTodos();
            ViewBag.InquilinoSelected = contrato.idInquilino;
            ViewBag.InmuebleSelected = contrato.idInmueble;
            ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
            return View(contrato);
        }

        if (repo.ExisteSuperposicion(contrato.idInmueble, contrato.fechaDesde, contrato.fechaHasta, null))
        {
            ModelState.AddModelError("", "Se superpone con otro contrato de este inmueble.");
            ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
            ViewBag.Usuarios = repoUsuario.ObtenerTodos();
            ViewBag.InquilinoSelected = contrato.idInquilino;
            ViewBag.InmuebleSelected = contrato.idInmueble;
            ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
            return View(contrato);
        }

        repo.Modificacion(contrato);
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Policy = "Administrador")]
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Renovar(int idContrato, DateTime fechaDesde, DateTime fechaHasta, decimal monto)
    {
        var actual = repo.ObtenerPorId(idContrato);
        if (actual == null) return NotFound();

        // Defensa en servidor: recalculo el "desde" por si tocaron el hidden en el cliente
        var desdeServidor = actual.fechaHasta.AddDays(1);

        if (fechaHasta < desdeServidor)
        {
            TempData["Error"] = $"La fecha 'Hasta' debe ser >= {desdeServidor:yyyy-MM-dd}.";
            return RedirectToAction(nameof(Index));
        }

        var nuevo = new Contrato
        {
            idInmueble = actual.idInmueble,
            idInquilino = actual.idInquilino,
            fechaDesde = desdeServidor,
            fechaHasta = fechaHasta,
            monto = monto,
            idUsuarioAlta = int.Parse(User.FindFirst("Id")!.Value),
            estado = true
        };


        if (repo.ExisteSuperposicion(nuevo.idInmueble, nuevo.fechaDesde, nuevo.fechaHasta, null))
        {
            TempData["Error"] = "No se puede renovar: el rango nuevo se superpone con otro contrato del mismo inmueble.";
            return RedirectToAction(nameof(Index));
        }

        repo.Alta(nuevo);
        TempData["Ok"] = "Contrato renovado con éxito.";
        return RedirectToAction(nameof(Index));
    }

}
