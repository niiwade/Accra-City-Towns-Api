namespace AccraCity.Application.Models;

public class Region
{
    public required Guid Id { get; set; }
    public required string RegionName { get; set; }
    public ICollection<District> Districts { get; set; } = new List<District>();    
    public ICollection<Town> Towns { get; set; } = new List<Town>();    
}