using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class inmuebleController : Controller
{
    private RepositorioInmueble repo;
    private RepositorioPropietario repoPropietario;

    public inmuebleController()
    {
        repo = new RepositorioInmueble();
        repoPropietario = new RepositorioPropietario();
    }

    /// Convierte un enum en una lista de SelectListItem.

    /// <typeparam name="TEnum">El tipo de enum a convertir.</typeparam>

    /// <returns>Una lista de SelectListItem.</returns>
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
        ViewBag.Propietarios = new RepositorioPropietario().ObtenerTodos();
        ViewBag.Usos = new SelectList(EnumToSelectList<UsoInmueble>(), "Value", "Text");
        ViewBag.Tipos = new SelectList(EnumToSelectList<TipoInmueble>(), "Value", "Text");
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
                return View(inmueble);
            }

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
            ViewBag.Propietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietarioSelected = entidad.idPropietario;
            ViewBag.Usos = new SelectList(EnumToSelectList<UsoInmueble>(), "Value", "Text", (int)entidad.uso);
            ViewBag.Tipos = new SelectList(EnumToSelectList<TipoInmueble>(), "Value", "Text", (int)entidad.tipo);
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
        ViewBag.nombrePropietario = propietario.nombre + " " + propietario.apellido;
        ViewBag.Usos = new SelectList(EnumToSelectList<UsoInmueble>(), "Value", "Text", (int)inmueble.uso);
        ViewBag.Tipos = new SelectList(EnumToSelectList<TipoInmueble>(), "Value", "Text", (int)inmueble.tipo);
        if (inmueble == null)
        {
            return NotFound();
        }

        return View(inmueble);
    }




}