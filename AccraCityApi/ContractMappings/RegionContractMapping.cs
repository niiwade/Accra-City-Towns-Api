using AccraCity.Application.Models;
using AccraCityApi.Contracts.Requests.RegionRequests;
using AccraCityApi.Contracts.Response.RegionResponses;

namespace AccraCityApi.ContractMappings;

public static class RegionContractMapping
{
    public static Region MapToRegion(this CreateRegionRequest request)  //This maps the CreateRegionDto to Region
    {
        return new Region()
        {
            Id = Guid.NewGuid(),
            RegionName = request.RegionName
        };
    
    }
    
    public static Region MapToRegion(this UpdateRegionRequest request, Guid id)  //This maps the UpdateRegionDto to Region
    {
        return new Region()
        {
            Id = id,
            RegionName = request.RegionName
        };
    
    }
    
    
    public static RegionResponse MapsToResponse(this Region region) //This maps the Region to RegionResponseDto
    {
        return new RegionResponse()
        {
            Id = region.Id,
            RegionName = region.RegionName
        };
    }
    
    public static RegionsResponse MapsToResponse(this IEnumerable<Region> regions) //This maps the list of districts to the DistrictsResponses
    {
        return new RegionsResponse()
        {
            Regions = regions.Select(MapsToResponse)
        };
    }
}