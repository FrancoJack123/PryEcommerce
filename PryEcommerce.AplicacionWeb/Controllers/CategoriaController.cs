using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PryEcommerce.Entidades;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

[Authorize(Roles = "ADMIN")]
public class CategoriaController : Controller
{
    private CategoriaServicio _categoriaServicio;

    public CategoriaController(CategoriaServicio c)
    {
        _categoriaServicio = c;
    }
    
    public IActionResult Index(int nropagina=0)
    {
        var listado = _categoriaServicio.Listar();

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
        var categoria = new Categoria();
        return View(categoria);
    }
    
    [HttpPost]
    public IActionResult Create(Categoria categoria)
    {
        try
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = _categoriaServicio.Guardar(categoria);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }

        return View(categoria);
    }

    public IActionResult Edit(int id)
    {
        var categoria = _categoriaServicio.Buscar(id);
        return View(categoria);
    }
    
    [HttpPost]
    public IActionResult Edit(Categoria categoria)
    {
        try
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = _categoriaServicio.Editar(categoria);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }
        
        return View(categoria);
    }

    public IActionResult Delete(int id)
    {
        TempData["danger"] = _categoriaServicio.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Deactivate(int id)
    {
        TempData["danger"] = _categoriaServicio.Desactivar(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Activate(int id)
    {
        TempData["success"] = _categoriaServicio.Activar(id);
        return RedirectToAction(nameof(Index));
    }
}