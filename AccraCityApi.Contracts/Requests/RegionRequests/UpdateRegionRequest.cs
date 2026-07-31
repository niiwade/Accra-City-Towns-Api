using System.ComponentModel.DataAnnotations;

namespace AccraCityApi.Contracts.Requests.RegionRequests;

public class UpdateRegionRequest
{
    [Required]
    [StringLength(100, ErrorMessage = "Region name cannot exceed 100 characters.")]
    public required string RegionName { get; set; }
}
