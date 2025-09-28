using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

[Authorize]
public class inmuebleController : Controller
{
    private RepositorioInmueble repo;
    private RepositorioPropietario repoPropietario;

    private RepositorioTipoInmueble repositorioTipoInmueble;

    public inmuebleController()
    {
        repo = new RepositorioInmueble();
        repoPropietario = new RepositorioPropietario();
        repositorioTipoInmueble = new RepositorioTipoInmueble();
    }

    private static IEnumerable<SelectListItem> EnumToSelectList<TEnum>() where TEnum : Enum =>
        Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new SelectListItem { Value = (Convert.ToInt32(e)).ToString(), Text = e.ToString() });


    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);


    }


    public IActionResult Create()
    {
        ViewBag.Propietarios = repoPropietario.ObtenerTodos();
        ViewBag.Tipos = repositorioTipoInmueble.ObtenerTodos();
        ViewBag.Usos = new SelectList(EnumToSelectList<UsoInmueble>(), "Value", "Text");
        return View();
    }

    [HttpPost]
    public IActionResult Create(Inmueble inmueble)
    {
        try
        {
            if (ModelState.IsValid)
            {
                repo.Alta(inmueble);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Propietarios = repoPropietario.ObtenerTodos();
                ViewBag.Tipos = repositorioTipoInmueble.ObtenerTodos();
                return View(inmueble);
            }

        }
        catch (Exception)
        {
            return View(inmueble);
        }

    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        try
        {
            repo.Baja(id);
            TempData["Ok"] = "Inmueble eliminado.";
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            TempData["Error"] = "No se puede eliminar: tiene contratos asociados.";
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el Inmueble.";
        }

        return RedirectToAction(nameof(Index));
    }



    public IActionResult Edit(int id)
    {
        try
        {
            var entidad = repo.ObtenerPorId(id);
            ViewBag.Propietarios = repoPropietario.ObtenerTodos();
            ViewBag.Tipos = repositorioTipoInmueble.ObtenerTodos();
            ViewBag.PropietarioSelected = entidad.idPropietario;
            ViewBag.TipoInmuebleSelected = entidad.idTipoInmueble;
            ViewBag.Usos = new SelectList(EnumToSelectList<UsoInmueble>(), "Value", "Text", (int)entidad.uso);
            return View("Edit", entidad);
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

    public IActionResult Detalles(int id)

    {
        var inmueble = repo.ObtenerPorId(id);
        var propietario = repoPropietario.ObtenerPorId(inmueble.idPropietario);
        var tipo = repositorioTipoInmueble.ObtenerPorId(inmueble.idTipoInmueble);
        ViewBag.nombrePropietario = propietario.nombre + " " + propietario.apellido;
        ViewBag.nombreTipoInmueble = tipo.nombre;
        // ViewBag.Usos = new SelectList(EnumToSelectList<UsoInmueble>(), "Value", "Text", (int)inmueble.uso);
        //  ViewBag.Tipos = new SelectList(EnumToSelectList<TipoInmueble>(), "Value", "Text", (int)inmueble.tipo);
        if (inmueble == null)
        {
            return NotFound();
        }

        return View(inmueble);
    }

    public IActionResult Disponibles()
    {
        var lista = repo.obtenerDisponibles();
        ViewBag.TiposInmuebles = repositorioTipoInmueble.ObtenerTodos();
        return View("Disponibles", lista);

    }

    public IActionResult DisponiblesFecha()
    {
        ViewBag.TiposInmuebles = repositorioTipoInmueble.ObtenerTodos();
        return View();

    }

    [HttpPost]
    public IActionResult DisponiblesFecha(string inicio, string final)
    {
        /*  if (string.IsNullOrEmpty(final))
         {
             throw new NullReferenceException();
         } */ //esto es para el video de debug
        var lista = repo.obtenerInmueblesDisponibles(inicio, final);
        ViewBag.TiposInmuebles = repositorioTipoInmueble.ObtenerTodos();
        return View("Disponibles", lista);
    }


}