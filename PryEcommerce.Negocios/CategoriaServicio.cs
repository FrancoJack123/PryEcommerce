using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura;
using PryEcommerce.Negocios.Interfaces;

namespace PryEcommerce.Negocios;

public class CategoriaServicio : IGenericService<Categoria>
{
    private CategoriaRepository _categoriaRepository;

    public CategoriaServicio(CategoriaRepository _categoriaRepository)
    {
        this._categoriaRepository = _categoriaRepository;
    }
    
    public List<Categoria> Listar()
    {
        return _categoriaRepository.Listar();
    }

    public Categoria Buscar(int id)
    {
        return _categoriaRepository.Buscar(id);
    }

    public string Guardar(Categoria t)
    {
        return _categoriaRepository.Guardar(t);
    }

    public string Editar(Categoria t)
    {
        return _categoriaRepository.Editar(t);
    }

    public string Eliminar(int id)
    {
        return _categoriaRepository.Eliminar(id);
    }

    public string Desactivar(int id)
    {
        return _categoriaRepository.Desactivar(id);
    }

    public string Activar(int id)
    {
        return _categoriaRepository.Activar(id);
    }
}