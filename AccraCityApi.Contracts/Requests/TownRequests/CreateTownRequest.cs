namespace AccraCityApi.Contracts.AccraCity;


public class CreateTownRequest
{
    public required string TownName { get; set; }
    public required string Category { get; set; }
    public required int Population { get; set; }
    public required float Latitude { get; set; }
    public required float Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
    //public DateTime LastModifiedAt { get; set; }
    public List<string> NearbyTowns { get; set; } = new List<string>();
    public List<string> NotableLandMarks { get; set; } = new List<string>();
    public Guid DistrictId { get; set; }
    public Guid RegionId { get; set; }
}