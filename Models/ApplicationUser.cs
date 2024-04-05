using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
public class ApplicationUser : IdentityUser {
    [Required]
    public required string? Name { get; set; }

    [Required]
    public required string? Surname { get; set; }
    
    [Required]
    public required string? Gender { get; set; }

    [Required]
    public required DateOnly DateOfBirth { get; set; }

    public static implicit operator string(ApplicationUser v)
    {
        throw new NotImplementedException();
    }
}