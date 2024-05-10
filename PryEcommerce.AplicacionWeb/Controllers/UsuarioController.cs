using Microsoft.AspNetCore.Mvc;
using PryEcommerce.Entidades;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

public class UsuarioController : Controller
{
    private UsuarioServicio _usuarioServicio;

    public UsuarioController(UsuarioServicio u)
    {
        _usuarioServicio = u;
    }
    
    public IActionResult Index(int nropagina=0)
    {
        var listado = _usuarioServicio.ListarRol("ADMIN");
        
        #region Paginacion

        int filas_pagina = 7;
        int cantidad = listado.Count;
        int paginas = 0;
        if (cantidad % filas_pagina > 0)
            paginas = (cantidad / filas_pagina) + 1;
        else
            paginas = cantidad / filas_pagina;
        ViewBag.paginas = paginas;
        ViewBag.nropagina = nropagina;
        ViewBag.cantidad = cantidad;

        #endregion
        
        return View(listado.Skip(nropagina*filas_pagina).Take(filas_pagina));
    }

    public IActionResult Create()
    {
        var user = new Usuario();
        return View(user);
    }

    [HttpPost]
    public IActionResult Create(Usuario user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = _usuarioServicio.Guardar(user);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }

        return View(user);
    }
    
    public IActionResult Delete(int id)
    {
        TempData["danger"] = _usuarioServicio.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }
}