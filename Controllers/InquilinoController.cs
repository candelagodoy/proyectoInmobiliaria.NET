using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class InquilinoController : Controller
{

    RepositorioInquilino repo;
    public InquilinoController()
    {
        repo = new RepositorioInquilino();
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
    public IActionResult Create(Inquilino inquilino)
    {
        try
        {
            repo.Alta(inquilino);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(inquilino);
        }
    }

    public IActionResult Eliminar(int id)
    {
        repo.Baja(id);
        return RedirectToAction(nameof(Index));
    }


    public ActionResult Edit(int id)
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
    public IActionResult Edit(Inquilino inquilino)
    {
        try
        {
            repo.Modificacion(inquilino);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(inquilino);
        }
    }






    

    



    



    

}
