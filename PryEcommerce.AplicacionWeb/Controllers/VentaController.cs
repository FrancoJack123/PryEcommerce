using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

[Authorize( Roles = "ADMIN")]
public class VentaController : Controller
{
    private VentaServicio _ventaServicio;

    public VentaController(VentaServicio v)
    {
        _ventaServicio = v;
    }
    
    public IActionResult Index(int nropagina = 0)
    {
        var listado = _ventaServicio.ListarDetalleVenta(0);
        
        #region Paginacion

        int filas_pagina = 6;
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
}