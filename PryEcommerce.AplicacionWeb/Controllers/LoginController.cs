using Microsoft.AspNetCore.Mvc;
using PryEcommerce.Entidades;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

public class LoginController : Controller
{
    private LoginServicio _loginServicio;
    
    public LoginController(LoginServicio s)
    {
        _loginServicio = s;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            TempData["danger"] = ViewBag.danger = "Por favor, complete todos los campos";
            return View();
        }
        
        var user = _loginServicio.LoginUsuario(email, password);
        
        if (user != null)
        {
            if (user.confirmar == true)
            {
                TempData["danger"] = $"Falta confirmar su cuenta. Se le envio un correo a '{email.ToUpper()}'";
                return View();
            }
            
            if (user.restablecer == true)
            {
                TempData["danger"] = $"Se ha solicitado restablecer su cuenta, favor de revisar su bandejade correo : '{email.ToUpper()}'";
                return View();
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.nombres),
                new Claim("Email", user.email),
                new Claim("codigo", user.id.ToString()),
                new Claim(ClaimTypes.Role, user.rol),
            };
            
            var clasimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(clasimsIdentity));

            if (user.rol != "ADMIN")
            {
                return RedirectToAction("Index","Shop");
            }
            
            return RedirectToAction("Index","Dashboard");
        }
        
        TempData["danger"] = "Credenciales incorrectas";
        return View();
    }
    
    public async Task<IActionResult> GoOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index","Shop");
    }
    
    public IActionResult Register()
    {
        var usuario = new Usuario();
        return View(usuario);
    }
    
    [HttpPost]
    public IActionResult Register(Usuario usuario)
    {
        try
        {
            if ( String.IsNullOrEmpty(usuario.nombres) || 
                 String.IsNullOrEmpty(usuario.apellidos) || 
                 String.IsNullOrEmpty(usuario.dni) || 
                 String.IsNullOrEmpty(usuario.telefono) ||
                 String.IsNullOrEmpty(usuario.email) ||
                 String.IsNullOrEmpty(usuario.password) ||
                 String.IsNullOrEmpty(usuario.confirmation_password)
            )
            {
                ViewBag.danger = "Por favor, complete todos los campos";
                return View(usuario);
            }
            
            if (usuario.password != usuario.confirmation_password)
            {
                ViewBag.danger = "Las contraseñas no coinciden";
                return View(usuario);
            }
            
            if (_loginServicio.VerificacionUsuario(usuario.email, usuario.dni))
            {
                ViewBag.danger = "El Correo electrónico o el DNI ya se encuentran registrados";
                return View(usuario);
            }
            
            TempData["success"] = _loginServicio.RegistrarUsuario(usuario);
            
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }
        
        return View(usuario);
    }
    
}