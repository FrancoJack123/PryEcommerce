using System.Data;
using System.Data.SqlClient;
using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura.Util.Encriptar;

namespace PryEcommerce.Infraestructura;

public class UsuarioRepository
{
    private string connectionString;

    public UsuarioRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    public List<Usuario> ListarRol(string rol)
    {
        var list = new List<Usuario>();
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_ListarUsuariosRol", rol))
        {
            while (dr.Read())
            {
                list.Add(new ()
                {
                    id = dr.GetInt32(0),
                    dni = dr.GetString(1),
                    nombres = dr.GetString(2),
                    apellidos = dr.GetString(3),
                    telefono = dr.GetString(4),
                    email = dr.GetString(5)
                });
            }
        }
        return list;
    }
    
    public string Guardar(Usuario t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_AgregarUsuarioAdmin",
            t.dni, t.nombres, t.apellidos, t.telefono, t.email, Encriptacion.EncriptarClave(t.password));
        return "El usuario fue registrado";
    }

    public string Eliminar(int id)
    {
        string mensaje;
        try
        {
            SqlHelper.ExecuteNonQuery(connectionString, "sp_EliminarUsuarioAdmin", id);
            mensaje = "El usuario fue eliminado";
        }
        catch (Exception e)
        {
            mensaje = "El usuario no se puede eliminar";
        }
        return mensaje;
    }
}