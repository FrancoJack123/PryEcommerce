namespace PryEcommerce.Entidades;

public class Producto
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public string url_img { get; set; }
    public int stock { get; set; }
    public decimal precio { get; set; }
    public bool estado { get; set; }
    public DateTime? fecha_ingreso { get; set; }
    public Categoria categoria { get; set; }
    public Marca marca { get; set; }
}