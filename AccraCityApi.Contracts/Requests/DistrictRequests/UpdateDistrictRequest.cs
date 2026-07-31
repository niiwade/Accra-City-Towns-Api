using System.ComponentModel.DataAnnotations;

namespace AccraCityApi.Contracts.Requests.DistrictRequests;

public class UpdateDistrictRequest
{
    [Required]
    [StringLength(100, ErrorMessage = "District name cannot exceed 100 characters.")]
    public required string DistrictName { get; set; }

    [Required]
    public required Guid RegionId { get; set; }
}
