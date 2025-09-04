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
        repo.Modificacion(new Propietario() { id = 2, nombre = "pepe", apellido = "perez", dni = "12345678", celular = "12345678", estado = true});
        //repo.Baja(6);
    
        var lista = repo.ObtenerTodos();
        return View(lista);
    }


}
