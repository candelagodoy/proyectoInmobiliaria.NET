using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class ContratoController : Controller
{
    private RepositorioContrato repo;
    private RepositorioInquilino repoInquilino;

    private RepositorioInmueble repoInmueble;

    public ContratoController()
    {
        repo = new RepositorioContrato();
        repoInquilino = new RepositorioInquilino();
        repoInmueble = new RepositorioInmueble();
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
        return View();
    }

    [HttpPost]
    public IActionResult Create(Contrato contrato)
    {
        try
        {
            repo.Alta(contrato);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(contrato);
        }
    }

    public IActionResult Edit(int id)
    {
        var contrato = repo.ObtenerPorId(id);
        ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
        return View(contrato);
    }

    [HttpPost]
    public IActionResult Edit(Contrato contrato)
    {
        try
        {
            repo.Modificacion(contrato);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(contrato);
        }
    }

    public IActionResult Eliminar(int id)
    {
        repo.Baja(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Detalles(int id)

    {
        var contrato = repo.ObtenerPorId(id);
        var inquilino = repoInquilino.ObtenerPorId(contrato.idInquilino);
        var inmueble = repoInmueble.ObtenerPorId(contrato.idInmueble);
        ViewBag.PropietarioNombre = inquilino.nombre + " " + inquilino.apellido;
        ViewBag.Inmueble = inmueble.direccion;
        if (contrato == null)
        {
        return NotFound();
        }

        return View(contrato);
    }
}
