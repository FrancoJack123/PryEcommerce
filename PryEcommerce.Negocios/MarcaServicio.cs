using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura;
using PryEcommerce.Negocios.Interfaces;

namespace PryEcommerce.Negocios;

public class MarcaServicio : IGenericService<Marca>
{
    private MarcaRepository _marcaRepository;

    public MarcaServicio(MarcaRepository _marcaRepository)
    {
        this._marcaRepository = _marcaRepository;
    }
    
    public List<Marca> Listar()
    {
        return _marcaRepository.Listar();
    }

    public Marca Buscar(int id)
    {
        return _marcaRepository.Buscar(id);
    }

    public string Guardar(Marca t)
    {
        return _marcaRepository.Guardar(t);
    }

    public string Editar(Marca t)
    {
        return _marcaRepository.Editar(t);
    }

    public string Eliminar(int id)
    {
        return _marcaRepository.Eliminar(id);
    }

    public string Desactivar(int id)
    {
        return _marcaRepository.Desactivar(id);
    }

    public string Activar(int id)
    {
        return _marcaRepository.Activar(id);
    }
}