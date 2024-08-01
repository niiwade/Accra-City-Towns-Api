using System.ComponentModel.DataAnnotations;

namespace AccraCity.Application.Dto;

public class UpdatePermissionDto
{
    [Required(ErrorMessage = "Username is required")]
    public required string UserName { get; set; }
}