namespace AccraCity.Application.Models;

public class District
{
    public required Guid Id { get; set; }
    public required string DistrictName { get; set; }
    public Guid RegionId { get; set; }
    public Region Region { get; set; } = null!;
    public ICollection<Town> Towns { get; set; } = new List<Town>();

}