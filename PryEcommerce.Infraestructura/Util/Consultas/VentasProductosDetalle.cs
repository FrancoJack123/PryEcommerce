namespace PryEcommerce.Infraestructura.Util.Consultas;

public class VentasProductosDetalle
{
    public string venta { get; set; }
    public string url_img { get; set; }
    public string producto { get; set; }
    public int cantidad { get; set; }
    public decimal precio_total { get; set; }
    public decimal monto { get; set; }
    public string usuario { get; set; }
}