using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PryEcommerce.Entidades;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

public class ShopController : Controller
{
    private ProductoServicio _productoServicio;
    private CategoriaServicio _categoriaServicio;
    private MarcaServicio _marcaServicio;
    private VentaServicio _ventaServicio;

    public ShopController(ProductoServicio c, CategoriaServicio p, MarcaServicio m, VentaServicio v)
    {
        _productoServicio = c;
        _categoriaServicio = p;
        _marcaServicio = m;
        _ventaServicio = v;
    }
    
    // GET
    public IActionResult Index(
        string nombre,
        int categoria_id,
        int marca_id,
        decimal precio_min,
        decimal precio_max,
        int nropagina = 0
        )
    {
        var listado = _productoServicio.FiltrarProducto(nombre, "", categoria_id, marca_id, precio_min, precio_max);
        
        #region Paginacion

        int filas_pagina = 5;
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

        var listaProductosPaginada  = listado.Skip(nropagina * filas_pagina).Take(filas_pagina);
        
        SetListadoProductos(listaProductosPaginada);

        ViewBag.categorias = _categoriaServicio.Listar().Where(p => p.estado == true);
        ViewBag.marcas = _marcaServicio.Listar().Where(p => p.estado == true);
        
        ViewBag.nombre = nombre;
        ViewBag.categoria_id = categoria_id;
        ViewBag.marca_id = marca_id;
        ViewBag.precio_min = precio_min;
        ViewBag.precio_max = precio_max;
        
        return View(listaProductosPaginada);
    }

    public IActionResult Details(int id)
    {
        var producto = _productoServicio.Buscar(id);
        ViewBag.list = _productoServicio
                            .FiltrarProducto("", "", producto.categoria.id, producto.marca.id, 0, 0)
                            .Take(4)
                            .Where(p => p.id != id)
                            .ToList();
        return View(producto);
    }
    
    public IActionResult CambiarVistaParcial(string vista)
    {
        var list = GetListadoProductos();
        if (vista == "PartialView1")
        {
            return PartialView("ViewPartialProducto/_ProductoFilter1", list);
        }
        else if (vista == "PartialView2")
        {
            return PartialView("ViewPartialProducto/_ProductoFilter2", list);
        }
        else if (vista == "PartialView3")
        {
            return PartialView("ViewPartialProducto/_ProductoFilter3", list);
        }
        else
        {
            return PartialView("ViewPartialProducto/_ProductoFilter2", list);
        }
    }

    public IActionResult CarritoVentas()
    {
        var list = GetCarritoVentas();
        return View(list);
    }

    public IActionResult AddCarritoVenta(int id, int cantidad = 1)
    {
        var prod = _productoServicio.Buscar(id);
        var listDetalleVenta = GetCarritoVentas().ToList();
        var detalle = listDetalleVenta.FirstOrDefault(venta => venta.producto.id == id);
        if (detalle != null)
        {
            if (detalle.cantidad == 1 && cantidad == -1)
                listDetalleVenta.Remove(detalle);
            else if (cantidad == -2)
                listDetalleVenta.Remove(detalle);
            else
                detalle.cantidad += cantidad;
        }
        else
        {
            listDetalleVenta.Add(new()
            {
                cantidad = cantidad,
                precio_unitario = prod.precio,
                nombre_producto = prod.nombre,
                url_img = prod.url_img,
                producto = prod
            });
        }

        HttpContext.Session.SetString("cantidad", listDetalleVenta.Count.ToString());
        SetCarritoVentas(listDetalleVenta);
        return RedirectToAction(nameof(CarritoVentas));
    }

    [Authorize]
    public IActionResult AllSaveCheckout()
    {
        try
        {
            var listadetalle = GetCarritoVentas().ToList();
            var monto = listadetalle.Sum(p => p.precio_total);
            var userIdClaim = int.Parse(HttpContext.User.FindFirst("codigo").Value);
            _ventaServicio.GrabarVenta(userIdClaim, monto, listadetalle);
            SetCarritoVentas(new List<DetalleVenta>());
            HttpContext.Session.SetString("cantidad", 0.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            TempData["message"] = e.Message;
        }
        return RedirectToAction(nameof(CarritoVentas));
    }

    [Authorize]
    public IActionResult AllOrders(int nropagina=0)
    {
        var userIdClaim = int.Parse(HttpContext.User.FindFirst("codigo").Value);
        var listado = _ventaServicio.ListarDetalleVenta(userIdClaim);
        
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
    
    private void SetListadoProductos(IEnumerable<Producto> list)
    {
        HttpContext.Session.SetString("list", JsonConvert.SerializeObject(list));
    }
    
    private IEnumerable<Producto> GetListadoProductos()
    {
        var listaSerializada = HttpContext.Session.GetString("list");
        if (listaSerializada != null)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Producto>>(listaSerializada);
        }
        else
        {
            return Enumerable.Empty<Producto>();
        }
    }
    
    private void SetCarritoVentas(IEnumerable<DetalleVenta> list)
    {
        HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(list));
    }
    
    private IEnumerable<DetalleVenta> GetCarritoVentas()
    {
        var listaSerializada = HttpContext.Session.GetString("carrito");
        if (listaSerializada != null)
        {
            return JsonConvert.DeserializeObject<IEnumerable<DetalleVenta>>(listaSerializada);
        }
        else
        {
            return Enumerable.Empty<DetalleVenta>();
        }
    }
}