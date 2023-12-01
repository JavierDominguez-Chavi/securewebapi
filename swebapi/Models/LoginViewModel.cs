namespace swebapi.Models;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [EmailAddress(ErrorMessage = "El campo {0} no es un correo válido")]
    public string Correo { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}