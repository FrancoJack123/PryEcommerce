namespace PryEcommerce.Negocios.Interfaces;

public interface IGenericService<T> where T:class
{
    public List<T> Listar();
    public T Buscar(int id);
    public string Guardar(T t);
    public string Editar(T t);
    public string Eliminar(int id);
    public string Desactivar(int id);
    public string Activar(int id);
}