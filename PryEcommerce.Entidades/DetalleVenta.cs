namespace PryEcommerce.Entidades;

public class DetalleVenta
{
    public int id { get; set; }
    public int cantidad { get; set; }
    public string nombre_producto { get; set; }
    public string url_img { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal precio_total { get; set; }
    public Venta venta { get; set; }
    public Producto producto { get; set; }
}