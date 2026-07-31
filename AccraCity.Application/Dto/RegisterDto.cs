using System.ComponentModel.DataAnnotations;

namespace AccraCity.Application.Dto;

public class RegisterDto
{
    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
