using System.ComponentModel.DataAnnotations;

public class Login {
 [Required(ErrorMessage = "Il campo Email è obbligatorio.")]
    [EmailAddress(ErrorMessage = "Il campo Email non è in un formato valido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Il campo Password è obbligatorio.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Ricordami")]
    public bool RememberMe { get; set; }
}