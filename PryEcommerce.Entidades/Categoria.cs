using System.ComponentModel.DataAnnotations;

namespace PryEcommerce.Entidades;

public class Categoria
{
    public int id { get; set; }
    
    [Required(ErrorMessage = "El campo es requerido")]
    public string nombre { get; set; }
    public bool? estado { get; set; }
}