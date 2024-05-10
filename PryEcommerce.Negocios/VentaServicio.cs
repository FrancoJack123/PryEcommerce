using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura;

namespace PryEcommerce.Negocios;

public class VentaServicio
{
    private VentaRepository _ventaRepository;

    public VentaServicio(VentaRepository _ventaRepository)
    {
        this._ventaRepository = _ventaRepository;
    }

    public void GrabarVenta(int usuario_id, decimal monto, List<DetalleVenta> detalleVentas)
    {
        _ventaRepository.GrabarVenta(usuario_id, monto, detalleVentas);
    }

    public List<DetalleVenta> ListarDetalleVenta(int id)
    {
        return _ventaRepository.ListarDetalleVenta(id);
    }
}