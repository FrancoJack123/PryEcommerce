using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura;
using PryEcommerce.Negocios.Interfaces;

namespace PryEcommerce.Negocios;

public class ProductoServicio : IGenericService<Producto>
{
    private ProductoRepository _productoRepository;

    public ProductoServicio(ProductoRepository _productoRepository)
    {
        this._productoRepository = _productoRepository;
    }

    public List<Producto> FiltrarProducto(
        string nombre,
        string descripcion,
        int categoria_id,
        int marca_id,
        decimal precio_min,
        decimal precio_max
    )
    {
        return _productoRepository.FiltrarProducto(nombre, descripcion, categoria_id, marca_id, precio_min, precio_max);
    }
    
    public List<Producto> Listar()
    {
        return _productoRepository.Listar();
    }

    public Producto Buscar(int id)
    {
        return _productoRepository.Buscar(id);
    }

    public string Guardar(Producto t)
    {
        return _productoRepository.Guardar(t);
    }

    public string Editar(Producto t)
    {
        return _productoRepository.Editar(t);
    }

    public string Eliminar(int id)
    {
        return _productoRepository.Eliminar(id);
    }

    public string Desactivar(int id)
    {
        return _productoRepository.Desactivar(id);
    }

    public string Activar(int id)
    {
        return _productoRepository.Activar(id);
    }

    public async Task<string> SubirImagen(Stream arch, string name)
    {
        return await _productoRepository.SubirImagen(arch, name);
    }
}