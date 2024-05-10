using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura;

namespace PryEcommerce.Negocios;

public class UsuarioServicio
{
    private UsuarioRepository _usuarioRepository;

    public UsuarioServicio(UsuarioRepository u)
    {
        _usuarioRepository = u;
    }

    public List<Usuario> ListarRol(string rol)
    {
        return _usuarioRepository.ListarRol(rol);
    }

    public string Guardar(Usuario t)
    {
        return _usuarioRepository.Guardar(t);
    }

    public string Eliminar(int id)
    {
        return _usuarioRepository.Eliminar(id);
    }
}