namespace AccraCity.Application.Models;

public class Town
{
    public required Guid Id { get; set; }
    public required string TownName { get; set; }
    public required string Category { get; set; }
    public required int Population { get; set; }
    public required float Latitude { get; set; }
    public required float Longitude { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public List<string> NearbyTowns { get; set; } = new List<string>();
    public List<string> NotableLandMarks { get; set; } = new List<string>();
    public Guid DistrictId { get; set; }
    public District District { get; set; } = null!;
    public Guid RegionId { get; set; }
    public Region Region { get; set; } = null!;

}