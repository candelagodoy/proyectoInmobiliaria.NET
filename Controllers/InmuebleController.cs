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

    
}