using System.Data.SqlClient;
using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura.Util;

namespace PryEcommerce.Infraestructura;

public class CategoriaRepository : IGenericRepository<Categoria>
{
    private string connectionString;

    public CategoriaRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    public List<Categoria> Listar()
    {
        var list = new List<Categoria>();
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_ListarCategorias"))
        {
            while (dr.Read())
            {
                list.Add(new ()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    estado = dr.GetBoolean(2)
                });
            }
        }
        return list;
    }

    public Categoria Buscar(int id)
    {
        Categoria categoria = null;
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_BuscarCategoria", id))
        {
            while (dr.Read())
            {
                categoria = new Categoria()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1)
                };
            }
        }
        return categoria;
    }

    public string Guardar(Categoria t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_AgregarCategoria", t.nombre);
        string message = $"La categoria : '{t.nombre.ToUpper()}' fue agregada con exito";
        return message;
    }

    public string Editar(Categoria t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_ActualizarCategoria", t.id, t.nombre);
        string message = $"La categoria : '{t.nombre.ToUpper()}' fue actualizada con exito";
        return message;
    }

    public string Eliminar(int id)
    {
        string message;
        try
        {
            SqlHelper.ExecuteNonQuery(connectionString, "sp_EliminarCategoria", id);
            message = "La categoria fue eliminada";
        }
        catch (Exception e)
        {
            message = "La categoria no se puede eliminar";
        }
        return message;
    }

    public string Desactivar(int id)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_EstadoCategoria", id, false);
        string message = $"La categoria : '{id}' fue desactivada";
        return message;
    }

    public string Activar(int id)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_EstadoCategoria", id, true);
        string message = $"La categoria : '{id}' fue activada";
        return message;
    }
}