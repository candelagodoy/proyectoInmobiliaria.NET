using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class PropietarioController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private RepositorioPropietario repo;
    public PropietarioController(ILogger<HomeController> logger)
    {
        _logger = logger;
        repo = new RepositorioPropietario();
    }

    public IActionResult Index()
    {
        repo.Alta(new Propietario() { nombre = "pepe", apellido = "perez", dni = "12345678", celular = "12345678", estado = true });
        var lista = repo.ObtenerTodos();
        return View(lista);
    }


}
