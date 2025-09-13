using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class inmuebleController : Controller
{
    private RepositorioInmueble repo;        

    public inmuebleController()
    {
        repo = new RepositorioInmueble();
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
    public IActionResult Create(Inmueble inmueble)
    {
        try{
            repo.Alta(inmueble);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(inmueble);
        }
        
    }

    public IActionResult Eliminar(int id)
    {
        repo.Baja(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        try
        {
            var entidad = repo.ObtenerPorId(id);
            return View("Edit",entidad);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public IActionResult Edit(Inmueble inmueble)
    {
        try
        {
            repo.Modificacion(inmueble);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(inmueble);
        }
    }
    


    
}