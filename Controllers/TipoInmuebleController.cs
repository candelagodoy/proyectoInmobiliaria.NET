using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

[Authorize]
public class TipoInmuebleController : Controller
{
    private RepositorioTipoInmueble repo;
    public TipoInmuebleController()
    {
        repo = new RepositorioTipoInmueble();
    }

    public IActionResult Index()
    {
        List<TipoInmueble> tiposInmuebles = repo.ObtenerTodos();
        return View(tiposInmuebles);

    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TipoInmueble tipoInmueble)
    {
        try
        {
            repo.Alta(tipoInmueble);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(tipoInmueble);
        }

    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Delete(int id)
    {
        try
        {
            repo.Baja(id);
            TempData["Ok"] = "Tipo de inmueble eliminado.";
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            TempData["Error"] = "No se puede eliminar: tiene inmuebles asociados.";
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el tipo de inmueble.";
        }

        return RedirectToAction(nameof(Index));
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
    public IActionResult Edit(TipoInmueble tipoInmueble)
    {
        try
        {
            repo.Modificacion(tipoInmueble);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(tipoInmueble);
        }
    }       


}