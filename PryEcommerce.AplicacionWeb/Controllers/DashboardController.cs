using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PryEcommerce.AplicacionWeb.Models;
using PryEcommerce.Negocios;

namespace PryEcommerce.AplicacionWeb.Controllers;

[Authorize(Roles = "ADMIN")]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private DashboardServicio _dashboardServicio;

    public DashboardController(ILogger<DashboardController> logger, DashboardServicio d)
    {
        _logger = logger;
        _dashboardServicio = d;
    }

    public IActionResult Index()
    {
        #region ObtenerDatosVentasPorProducto

        var procVentasProdc = new List<string>();
        var ventVentasProdc = new List<int>();
        
        _dashboardServicio.ObtenerDatosVentasPorProducto(procVentasProdc, ventVentasProdc);

        ViewBag.procVentasProdc = procVentasProdc;
        ViewBag.ventVentasProdc = ventVentasProdc;

        #endregion
        
        #region ObtenerDatosProductosEnStock

        var procProdcStock = new List<string>();
        var stckProdcStock = new List<int>();
        
        _dashboardServicio.ObtenerDatosProductosEnStock(procProdcStock, stckProdcStock);

        ViewBag.procProdcStock = procProdcStock;
        ViewBag.stckProdcStock = stckProdcStock;

        #endregion

        ViewBag.VentasProductosDetalle = _dashboardServicio.VentasProductosDetalle();
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}