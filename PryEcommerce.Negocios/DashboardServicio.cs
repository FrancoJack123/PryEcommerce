using PryEcommerce.Infraestructura;
using PryEcommerce.Infraestructura.Util.Consultas;

namespace PryEcommerce.Negocios;

public class DashboardServicio
{
    private DashboardRepository _dashboardRepository;

    public DashboardServicio(DashboardRepository d)
    {
        _dashboardRepository = d;
    }

    public void ObtenerDatosVentasPorProducto(List<String> productos, List<int> ventas)
    {
        _dashboardRepository.ObtenerDatosVentasPorProducto(productos, ventas);
    }

    public void ObtenerDatosProductosEnStock(List<String> productos, List<int> stock)
    {
        _dashboardRepository.ObtenerDatosProductosEnStock(productos, stock);
    }

    public List<VentasProductosDetalle> VentasProductosDetalle()
    {
        return _dashboardRepository.VentasProductosDetalle();
    }
}