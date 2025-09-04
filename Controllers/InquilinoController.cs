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

}
