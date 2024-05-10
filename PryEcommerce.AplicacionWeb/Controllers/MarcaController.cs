using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PryEcommerce.Entidades;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

[Authorize(Roles = "ADMIN")]
public class MarcaController : Controller
{
    private MarcaServicio _marcaServicio;

    public MarcaController(MarcaServicio m)
    {
        _marcaServicio = m;
    }
    
    public IActionResult Index(int nropagina=0)
    {
        var listado = _marcaServicio.Listar();

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
        var marca = new Marca();
        return View(marca);
    }
    
    [HttpPost]
    public IActionResult Create(Marca marca)
    {
        try
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = _marcaServicio.Guardar(marca);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }

        return View(marca);
    }

    public IActionResult Edit(int id)
    {
        var marca = _marcaServicio.Buscar(id);
        return View(marca);
    }
    
    [HttpPost]
    public IActionResult Edit(Marca marca)
    {
        try
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = _marcaServicio.Editar(marca);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }
        
        return View(marca);
    }

    public IActionResult Delete(int id)
    {
        TempData["danger"] = _marcaServicio.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Deactivate(int id)
    {
        TempData["danger"] = _marcaServicio.Desactivar(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Activate(int id)
    {
        TempData["success"] = _marcaServicio.Activar(id);
        return RedirectToAction(nameof(Index));
    }
    
}