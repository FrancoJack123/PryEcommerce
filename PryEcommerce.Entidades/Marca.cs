using System.ComponentModel.DataAnnotations;

namespace PryEcommerce.Entidades;

public class Marca
{
    public int id { get; set; }

    [Required(ErrorMessage = "El campo es requerido")]
    public string nombre { get; set; } = String.Empty;
    public bool? estado { get; set; }
}