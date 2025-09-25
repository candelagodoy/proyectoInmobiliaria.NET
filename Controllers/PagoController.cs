using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Iana;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

public class pagoController : Controller
{
    private RepositorioPago repoPago;
    private RepositorioContrato repoContrato;
    private RepositorioUsuario repoUsuario;

    public pagoController()
    {
        repoPago = new RepositorioPago();
        repoContrato = new RepositorioContrato();
        repoUsuario = new RepositorioUsuario();

    }

    public IActionResult Index()
    {
        var lista = repoPago.ObtenerTodos();
        return View(lista);
    }

    
    public IActionResult Create()
    {
        ViewBag.Contratos = repoContrato.ObtenerTodos();
        ViewBag.Usuarios = repoUsuario.ObtenerTodos();
        return View();
    }


    [HttpPost]
    public IActionResult Create(Pago pago)
    {
        try
        {
            if (ModelState.IsValid)
            {
                repoPago.Alta(pago);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Contratos = repoContrato.ObtenerTodos();
                ViewBag.Usuarios = repoUsuario.ObtenerTodos();
                return View(pago);
            }

        }
        catch (Exception)
        {
            return View(pago);
        }

    }

    






}