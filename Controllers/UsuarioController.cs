
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

    private readonly IWebHostEnvironment environment;
    private RepositorioUsuario repo;

    public UsuarioController(IWebHostEnvironment env)
    {
        environment = env;
        repo = new RepositorioUsuario();
    }
    [Authorize(Policy = "Administrador")]
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
                    ModelState.AddModelError(string.Empty, "El email y/o el password son incorrectos");
                    return View();
                }
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, e.email),
                        new Claim("FullName", e.nombre + " " + e.apellido),
                        new Claim(ClaimTypes.Role, e.rolNombre),
                        new Claim("Id", e.idUsuario.ToString())
                    };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction(nameof(Index), "Home");
            }
            if ( login.Clave == null || login.Email == null )
                {
                    ModelState.AddModelError(string.Empty, "Complete todos los campos");
                    return View();
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
    public IActionResult Create(Usuario u)
    {

        if (!ModelState.IsValid) return View(u);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: u.clave,
            salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));
        u.clave = hashed;

        var id = repo.Alta(u);
        u.idUsuario = id;

        if (u.avatarFile is { Length: > 0 })
        {

            var wwwPath = environment.WebRootPath;
            var folderName = "Uploads"; // mantenemos el mismo nombre que usabas
            var physicalFolder = Path.Combine(wwwPath, folderName);
            if (!Directory.Exists(physicalFolder))
                Directory.CreateDirectory(physicalFolder);

            var ext = Path.GetExtension(u.avatarFile.FileName);

            var fileName = $"avatar_{id}_{Guid.NewGuid():N}{ext}";
            var physicalPath = Path.Combine(physicalFolder, fileName);

            using var stream = new FileStream(physicalPath, FileMode.Create);
            u.avatarFile.CopyTo(stream);

            u.avatar = $"/{folderName}/{fileName}";

            repo.Modificacion(u);
        }

        return RedirectToAction(nameof(Index));
    }






    [Authorize]
    public IActionResult Edit(int id)
    {
        var db = repo.ObtenerPorId(id);
        if (db is null) return NotFound();

        if (User.IsInRole("Administrador"))
        {

            return View(db);
        }
        else
        {
            if (id != int.Parse(User.FindFirst("Id")?.Value))
            {
                return RedirectToAction("Restringido", "Home");

            }
            else
            {
                return View(db);
            }
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(Usuario i, bool eliminarAvatar = false)
    {
        var esAdmin = User.IsInRole("Administrador");
        if (!esAdmin)
        {
            if (i.idUsuario != int.Parse(User.FindFirst("Id")?.Value))
                return RedirectToAction("Restringido", "Home");
        }

        if (string.IsNullOrWhiteSpace(i.clave))
        {
            ModelState.Remove(nameof(i.clave));
            ModelState.ClearValidationState(nameof(i.clave));
        }
        if (!esAdmin)
        {
            foreach (var k in new[] { nameof(i.rol) })
            {
                ModelState.Remove(k);
                ModelState.ClearValidationState(k);
            }
        }
        if (!ModelState.IsValid) return View(i);

        var db = repo.ObtenerPorId(i.idUsuario);
        if (db is null) return NotFound();


        if (esAdmin) db.rol = i.rol;

        db.nombre = i.nombre;
        db.apellido = i.apellido;
        db.email = i.email;

    
        if (!string.IsNullOrWhiteSpace(i.clave))
        {
            db.clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: i.clave,
                salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
        }

        var hayArchivoNuevo = i.avatarFile is { Length: > 0 };
        if (eliminarAvatar && !hayArchivoNuevo && !string.IsNullOrWhiteSpace(db.avatar))
        {
            var oldPath = Path.Combine(environment.WebRootPath, db.avatar.TrimStart('/', '\\'));
            if (System.IO.File.Exists(oldPath))
            {
                try { System.IO.File.Delete(oldPath); } catch { /* log si querés */ }
            }
            db.avatar = null;
        }


        if (hayArchivoNuevo)
        {
            var wwwPath = environment.WebRootPath;
            var folderName = "Uploads";
            var physicalDir = Path.Combine(wwwPath, folderName);
            if (!Directory.Exists(physicalDir)) Directory.CreateDirectory(physicalDir);

            var ext = Path.GetExtension(i.avatarFile.FileName);
            var fileName = $"avatar_{db.idUsuario}_{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(physicalDir, fileName);

            using (var fs = new FileStream(fullPath, FileMode.Create))
                await i.avatarFile.CopyToAsync(fs);

            // borrar anterior (si existía)
            if (!string.IsNullOrWhiteSpace(db.avatar))
            {
                var old = Path.Combine(environment.WebRootPath, db.avatar.TrimStart('/', '\\'));
                if (System.IO.File.Exists(old))
                {
                    try { System.IO.File.Delete(old); } catch { /* log */ }
                }
            }

            db.avatar = $"/{folderName}/{fileName}";
        }

        repo.Modificacion(db);

        if (!esAdmin)
        {
            var id = int.Parse(User.FindFirst("Id")?.Value);
            return RedirectToAction(nameof(Details), new { id });
        }
        return RedirectToAction(nameof(Index));
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
         if (!User.IsInRole("Administrador"))
        {
            var idLog = int.Parse(User.FindFirst("Id")?.Value);
            if (id != idLog)
                return RedirectToAction("Restringido", "Home");
        }
        Usuario u = repo.ObtenerPorId(id);
        ViewBag.UsuarioLogin = repo.ObtenerPorId(int.Parse(User.FindFirst("Id")?.Value));
        return View(u);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EliminarAvatar(int id)
    {
        var usuario = repo.ObtenerPorId(id);
        if (usuario == null) return NotFound();

        usuario.avatar = null; // o ruta por defecto
        repo.Modificacion(usuario);

        return RedirectToAction("Edit", new { id = id });
    }



}

