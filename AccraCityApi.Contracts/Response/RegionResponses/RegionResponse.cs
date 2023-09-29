using AccraCity.Application.Models;

namespace AccraCityApi.Contracts.Response.RegionResponses;
public class RegionResponse
{
    public required Guid Id { get; set; }
    public required string RegionName { get; set; }
    // public ICollection<District> Districts { get; set; } = new List<District>();    
    // public ICollection<Town> Towns { get; set; } = new List<Town>();   

}