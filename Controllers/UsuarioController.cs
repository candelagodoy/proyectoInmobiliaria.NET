
using proyectoInmobiliaria.NET.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace proyectoInmobiliaria.NET.Controllers;

[Authorize]
public class UsuarioController : Controller
{

    private RepositorioUsuario repo;

    public UsuarioController()
    {
        repo = new RepositorioUsuario();
    }
    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    [AllowAnonymous]
    public ActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(Login login)
    {
        try
        {
            if (ModelState.IsValid)
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: login.Clave,
                 salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                 prf: KeyDerivationPrf.HMACSHA1,
                 iterationCount: 1000,
                 numBytesRequested: 256 / 8));

                var e = repo.ObtenerPorEmail(login.Email);
                if (e == null || e.clave != hashed)
                {
                    ModelState.AddModelError("", "El email y/o el password son incorrectos");
                    return View();
                }
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, e.email),
                        new Claim("FullName", e.nombre + " " + e.apellido),
                        new Claim(ClaimTypes.Role, e.rolNombre),
                    };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction(nameof(Index), "Home");
            }
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Usuario");
    }

    [Authorize(Policy = "Administrador")]
    public ActionResult Create()
    {
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Administrador")]
    public ActionResult Create(Usuario u)
    {
        if (!ModelState.IsValid)
            return View();
        try
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: u.clave,
                    salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
            u.clave = hashed;

            var nbreRnd = Guid.NewGuid();
            repo.Alta(u);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.StackTrate = ex.StackTrace;
            return View();
        }
    }



    [Authorize(Policy = "Administrador")]
    public ActionResult Edit(int id)
    {
        var i = repo.ObtenerPorId(id);
        return View(i);
    }

    // POST: Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Administrador")]
    public ActionResult Edit(int id, Usuario i)
    {
        try
        {
            //if (ModelState.IsValid)
            {

                i.clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: i.clave,
                    salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                repo.Modificacion(i);
                return RedirectToAction(nameof(Index));

            }
            /*else
            {
                ModelState.AddModelError("", "Error al actualizar!!");
                return View();
            }*/

        }
        catch
        {
            return View();
        }
    }

    // GET: Admin/Delete/5
    [Authorize(Policy = "Administrador")]
    public ActionResult Delete(int id)
    {
        var i = repo.ObtenerPorId(id);
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
            repo.Baja(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
        
    public ActionResult Details(int id)
        {
            Usuario u = repo.ObtenerPorId(id);
            return View(u);
        }


}
