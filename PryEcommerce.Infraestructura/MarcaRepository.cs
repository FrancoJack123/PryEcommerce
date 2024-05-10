using System.Data.SqlClient;
using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura.Util;

namespace PryEcommerce.Infraestructura;

public class MarcaRepository : IGenericRepository<Marca>
{
    private string connectionString;

    public MarcaRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    public List<Marca> Listar()
    {
        var list = new List<Marca>();
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_ListarMarcas"))
        {
            while (dr.Read())
            {
                list.Add(new Marca()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    estado = dr.GetBoolean(2)
                });
            }
        }
        return list;
    }

    public Marca Buscar(int id)
    {
        Marca marca = null;
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_BuscarMarca", id))
        {
            while (dr.Read())
            {
                marca = new Marca()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    estado = dr.GetBoolean(2)
                };
            }
        }
        return marca;
    }

    public string Guardar(Marca t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_AgregarMarca", t.nombre);
        string message = "La marca fue agregada";
        return message;
    }

    public string Editar(Marca t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_ActualizarMarca", t.id, t.nombre);
        string message = "La marca fue actualizada";
        return message;
    }

    public string Eliminar(int id)
    {
        string message;
        try
        {
            SqlHelper.ExecuteNonQuery(connectionString, "sp_EliminarMarca", id);
            message = "La marca fue eliminada";
        }
        catch (Exception e)
        {
            message = "La marca no se puede eliminar";
        }
        return message;
    }

    public string Desactivar(int id)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_EstadoMarca", id, false);
        string message = "La marca fue desactivada";
        return message;
    }

    public string Activar(int id)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_EstadoMarca", id, true);
        string message = "La marca fue activada";
        return message;
    }
}