using System.Data.SqlClient;
using PryEcommerce.Infraestructura.Util.Consultas;

namespace PryEcommerce.Infraestructura;

public class DashboardRepository
{
    private string connectionString;

    public DashboardRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void ObtenerDatosVentasPorProducto(List<String> productos, List<int> ventas)
    {
        using (SqlDataReader rd = SqlHelper.ExecuteReader(connectionString, "sp_ObtenerDatosVentasPorProducto"))
        {
            while (rd.Read())
            {
                productos.Add(rd.GetString(0));
                ventas.Add(rd.GetInt32(1));
            }
        }
    }
    
    public void ObtenerDatosProductosEnStock(List<String> productos, List<int> stock)
    {
        using (SqlDataReader rd = SqlHelper.ExecuteReader(connectionString, "sp_ObtenerDatosProductosEnStock"))
        {
            while (rd.Read())
            {
                productos.Add(rd.GetString(0));
                stock.Add(rd.GetInt32(1));
            }
        }
    }

    public List<VentasProductosDetalle> VentasProductosDetalle()
    {
        var list = new List<VentasProductosDetalle>();
        
        using (SqlDataReader rd = SqlHelper.ExecuteReader(connectionString, "sp_VentasProductosDetalle"))
        {
            while (rd.Read())
            {
                list.Add(new()
                {
                    venta = rd.GetString(0),
                    url_img = rd.GetString(1),
                    producto = rd.GetString(2),
                    cantidad = rd.GetInt32(3),
                    precio_total = rd.GetDecimal(4),
                    monto = rd.GetDecimal(5),
                    usuario = rd.GetString(6)
                });
            }
        }

        return list;
    }
}