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
        //var lista = repo.ObtenerTodos();
        /*var inmueble = new Inmueble {
        idInmueble = 1,
        direccion = "chacabuco",
        uso = "Residencial",
        tipo = "depto",
        cantidadAmb = 3,
        coordenadas = "-34.6037, -58.3816",
        precio = 120000,
        idPropietario = 5,
        estado = true
        };*/
        //repo.Alta(inmueble);
        //repo.Modificacion(inmueble);
        repo.Baja(1);
        return View();
        
    }

    
}