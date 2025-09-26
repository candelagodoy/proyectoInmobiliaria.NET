using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class PropietarioController : Controller
{
    private RepositorioPropietario repo;

    public PropietarioController()
    {
        repo = new RepositorioPropietario();
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Propietario propietario)
    {
        try
        {
            repo.Alta(propietario);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(propietario);
        }
    }

    public ActionResult Edit(int id)
    {
        try
        {
            var entidad = repo.ObtenerPorId(id);
            return View("Edit", entidad);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public ActionResult Edit(Propietario propietario)
    {
        try
        {
            repo.Modificacion(propietario);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(propietario);
        }
    }

    public IActionResult Eliminar(int id)
    {
        try
        {
            repo.Baja(id);
            TempData["Ok"] = "Propietario eliminado.";
        }
        catch (MySqlException ex) when (ex.Number == 1451) // FK: tiene datos relacionados
        {
            TempData["Error"] = "No se puede eliminar: tiene inmuebles y/o contratos asociados.";
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el propietario.";
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Detalles(int id)

    {
        var propietario = repo.ObtenerPorId(id);
        if (propietario == null)
        {
            return NotFound();
        }

        return View(propietario);
    }













}
