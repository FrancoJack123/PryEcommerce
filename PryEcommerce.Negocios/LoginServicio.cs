using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura;

namespace PryEcommerce.Negocios;

public class LoginServicio
{
    private LoginRepository _loginRepository;

    public LoginServicio(LoginRepository _loginRepository)
    {
        this._loginRepository = _loginRepository;
    }

    public Usuario LoginUsuario(string email, string password)
    {
        return _loginRepository.LoginUsuario(email, password);
    }

    public string RegistrarUsuario(Usuario u)
    {
        return _loginRepository.RegistrarUsuario(u);
    }

    public bool VerificacionUsuario(string email, string dni)
    {
        return _loginRepository.VerificacionUsuario(email, dni);
    }
}