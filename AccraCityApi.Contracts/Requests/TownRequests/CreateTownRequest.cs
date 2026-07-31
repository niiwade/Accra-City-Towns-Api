using System.ComponentModel.DataAnnotations;

namespace AccraCityApi.Contracts.AccraCity;


public class CreateTownRequest
{
    [Required]
    [StringLength(100, ErrorMessage = "Town name cannot exceed 100 characters.")]
    public required string TownName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
    public required string Category { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Population must be a non-negative number.")]
    public required int Population { get; set; }

    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
    public required double Latitude { get; set; }

    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
    public required double Longitude { get; set; }

    public List<string> NearbyTowns { get; set; } = new List<string>();
    public List<string> NotableLandMarks { get; set; } = new List<string>();

    [Required]
    public required Guid DistrictId { get; set; }

    [Required]
    public required Guid RegionId { get; set; }
}
