namespace AccraCity.Application.Models;

public class Town
{
    public required Guid Id { get; set; }
    public required string TownName { get; set; }
    public required string Category { get; set; }
    public required int Population { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public List<string> NearbyTowns { get; set; } = [];
    public List<string> NotableLandMarks { get; set; } = [];
    public Guid DistrictId { get; set; }
    public District District { get; set; } = null!;
    public Guid RegionId { get; set; }
    public Region Region { get; set; } = null!;

}
