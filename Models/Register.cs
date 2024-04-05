using System.ComponentModel.DataAnnotations;

public class Register {
[Required(ErrorMessage = "Il campo Email è obbligatorio.")]
    [EmailAddress(ErrorMessage = "Il campo Email non è in un formato valido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Il campo Password è obbligatorio.")]
    [StringLength(100, ErrorMessage = "La password deve essere lunga almeno {2} caratteri.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Conferma password")]
    [Compare("Password", ErrorMessage = "La password e la conferma password non corrispondono.")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
    public string? Surname { get; set; }
    
    [Required(ErrorMessage = "Il campo Genere è obbligatorio.")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "Il campo Data di nascita è obbligatorio.")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}