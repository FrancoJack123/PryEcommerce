namespace PryEcommerce.Entidades;

public class Usuario
{
    public int id { get; set; }
    public string dni { get; set; }
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string telefono { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string rol { get; set; }
    public bool? restablecer { get; set; }
    public bool? confirmar { get; set; }
    public string? token { get; set; }
}