using System.Data.SqlClient;
using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura.Util.Encriptar;
using PryEcommerce.Infraestructura.Util.Token;

namespace PryEcommerce.Infraestructura;

public class LoginRepository
{
    private string connectionString;

    public LoginRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public Usuario LoginUsuario(string email, string password)
    {
        Usuario user = null;
        using (SqlDataReader rd = SqlHelper.ExecuteReader(connectionString, "sp_Login", email, Encriptacion.EncriptarClave(password)))
        {
            while (rd.Read())
            {
                user = new Usuario();
                user.id = rd.GetInt32(0);
                user.nombres = rd.GetString(1);
                user.email = rd.GetString(2);
                user.apellidos = rd.GetString(3);
                user.rol = rd.GetString(4);
                user.confirmar = rd.GetBoolean(5);
                user.restablecer = rd.GetBoolean(6);
            }
            rd.Close();
        }
        return user;
    }

    public string RegistrarUsuario(Usuario u)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_RegistrarUsuario",
            u.dni, u.nombres, u.apellidos, u.telefono, u.email, Encriptacion.EncriptarClave(u.password), u.rol, Token.GenerarToken());
        //message = $"El usuario '{u.dni}' ha sido registrado con éxito. Por favor, revise su bandeja de correo electrónico para confirmar su cuenta.";
        string message = "Su usuario ha sido registrado";
        return message;
    }
    
    public bool VerificacionUsuario(string email, string dni)
    {
        bool status = false;
        using (SqlDataReader rd = SqlHelper.ExecuteReader(connectionString, "sp_Verificacion", email, dni))
        {
            while (rd.Read())
            {
                status = true;
            }
            rd.Close();
        }
        return status;
    }
    
    
}