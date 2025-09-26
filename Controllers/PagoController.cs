using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Iana;
using proyectoInmobiliaria.NET.Models;

namespace proyectoInmobiliaria.NET.Controllers;

[Authorize]
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
        ViewBag.UsuarioLogin = repoUsuario.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
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

    [Authorize(Policy = "Administrador")]
    public ActionResult Delete(int id)
    {
        var i = repoPago.ObtenerPorId(id);
        return View(i);
    }

    // POST: Admin/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Administrador")]
    public ActionResult Delete(int id, Usuario i)
    {
        try
        {
            repoPago.Baja(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
    public IActionResult Edit(int id)
    {
        var pago = repoPago.ObtenerPorContrato(id);
        ViewBag.Contratos = repoContrato.ObtenerTodos();
        ViewBag.Usuarios = repoUsuario.ObtenerTodos();
        return View("Edit", pago);
    }
}