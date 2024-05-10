using System.Data;
using System.Data.SqlClient;
using PryEcommerce.Entidades;

namespace PryEcommerce.Infraestructura;

public class VentaRepository
{
    private string connectionString;

    public VentaRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void GrabarVenta(int usuario_id, decimal monto, List<DetalleVenta> detalleVentas)
    {
        /*string ventaId = SqlHelper.ExecuteScalar(
            connectionString, 
            "sp_GrabarVentas", 
            usuario_id, monto)
            .ToString();

        foreach (var detalleVenta in detalleVentas)
        {
            SqlHelper.ExecuteNonQuery(connectionString, "sp_GrabarVentasDetalle",
                int.Parse(ventaId), detalleVenta.cantidad, detalleVenta.nombre_producto,
                detalleVenta.url_img, detalleVenta.precio_unitario, detalleVenta.precio_total, detalleVenta.producto.id);
        }*/
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string ventaId;
                using (SqlCommand command = new SqlCommand("sp_GrabarVentas", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@monto", monto);
                    ventaId = command.ExecuteScalar().ToString();
                }
                foreach (var detalleVenta in detalleVentas)
                {
                    using (SqlCommand command = new SqlCommand("sp_GrabarVentasDetalle", connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@venta_id", int.Parse(ventaId));
                        command.Parameters.AddWithValue("@cantidad", detalleVenta.cantidad);
                        command.Parameters.AddWithValue("@nombre_producto", detalleVenta.nombre_producto);
                        command.Parameters.AddWithValue("@url_img", detalleVenta.url_img);
                        command.Parameters.AddWithValue("@precio_unitario", detalleVenta.precio_unitario);
                        command.Parameters.AddWithValue("@precio_total", detalleVenta.precio_total);
                        command.Parameters.AddWithValue("@producto_id", detalleVenta.producto.id);
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }

    public List<DetalleVenta> ListarDetalleVenta(int id)
    {
        var list = new List<DetalleVenta>();

        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_ListarDetalleVenta", id))
        {
            while (dr.Read())
            {
                var detalleVenta = new DetalleVenta()
                {
                    cantidad = dr.GetInt32(1),
                    id = dr.GetInt32(0),
                    nombre_producto = dr.GetString(2),
                    url_img = dr.GetString(3),
                    precio_unitario = dr.GetDecimal(4),
                    venta = new Venta()
                    {
                        id = dr.GetInt32(6),
                        fecha_venta = dr.GetDateTime(12),
                        codigo = dr.GetString(13),
                        usuario = new Usuario()
                        {
                            id = dr.GetInt32(8),
                            nombres = dr.GetString(11),
                        }
                    },
                    producto = new Producto()
                    {
                        id = dr.GetInt32(7),
                        categoria = new Categoria()
                        {
                            nombre = dr.GetString(9)
                        },
                        marca = new Marca()
                        {
                            nombre = dr.GetString(10)
                        }
                    }
                };

                list.Add(detalleVenta);
            }
        }

        return list;
    }
}