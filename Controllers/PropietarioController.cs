using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

    

    



  


}
