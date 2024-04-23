namespace PryEcommerce.Entidades;

public class Venta
{
    public int id { get; set; }
    public DateTime fecha_venta { get; set; }
    public decimal monto { get; set; }
    public Usuario usuario { get; set; }
}