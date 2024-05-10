using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PryEcommerce.Entidades;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

[Authorize(Roles = "ADMIN")]
public class ProductoController : Controller
{
    private ProductoServicio _productoServicio;
    private CategoriaServicio _categoriaServicio;
    private MarcaServicio _marcaServicio;

    public ProductoController(ProductoServicio c, CategoriaServicio p, MarcaServicio m)
    {
        _productoServicio = c;
        _categoriaServicio = p;
        _marcaServicio = m;
    }
    
    public IActionResult Index(int nropagina=0)
    {
        var listado = _productoServicio.Listar();

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
        var producto = new Producto();
        ViewBag.categorias = new SelectList(_categoriaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
        ViewBag.marcas = new SelectList(_marcaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
        return View(producto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Producto producto, IFormFile img)
    {
        try
        {
            ViewBag.categorias = new SelectList(_categoriaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
            ViewBag.marcas = new SelectList(_marcaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
            
            if (img == null || img.Length <= 0)
            {
                ViewBag.danger = "La imagen es requerida";
                return View(producto);
            }
            
            Stream image = img.OpenReadStream();
            producto.url_img = await _productoServicio.SubirImagen(image, img.FileName);
            
            if (ModelState.IsValid)
            {
                TempData["success"] = _productoServicio.Guardar(producto);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }
        
        return View(producto);
    }

    public IActionResult Edit(int id)
    {
        var producto = _productoServicio.Buscar(id);
        ViewBag.categorias = new SelectList(_categoriaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
        ViewBag.marcas = new SelectList(_marcaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
        return View(producto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Producto producto, IFormFile img)
    {
        try
        {
            ModelState.Remove("img");
            
            if (img != null && img.Length > 0)
            {
                Stream image = img.OpenReadStream();
                producto.url_img = await _productoServicio.SubirImagen(image, img.FileName);
            }
            
            if (ModelState.IsValid)
            {
                TempData["success"] = _productoServicio.Editar(producto);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ViewBag.danger = e.Message;
        }
        
        ViewBag.categorias = new SelectList(_categoriaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
        ViewBag.marcas = new SelectList(_marcaServicio.Listar().Where(p => p.estado == true), "id", "nombre");
        
        return View(producto);
    }

    public IActionResult Delete(int id)
    {
        TempData["danger"] = _productoServicio.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Deactivate(int id)
    {
        TempData["danger"] = _productoServicio.Desactivar(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Activate(int id)
    {
        TempData["success"] = _productoServicio.Activar(id);
        return RedirectToAction(nameof(Index));
    }
}