using AccraCity.Application.Models;
using AccraCityApi.Contracts.AccraCity;
using AccraCityApi.Contracts.Requests.TownRequests;
using AccraCityApi.Contracts.Response.TownResponses;

namespace AccraCityApi.ContractMappings;

public static class TownContractMapping
{
    public static Town MapToTown(this CreateTownRequest request)  //This maps the CreateTownDto to Town
    {
        return new Town()
        {
            Id = Guid.NewGuid(),
            TownName = request.TownName,
            Category = request.Category,
            Population =request.Population,
            Latitude = request.Latitude,
            Longitude =request.Longitude,
            StartDateTime = request.StartDateTime,
            LastModifiedDateTime  = request.LastModifiedDateTime,
            NearbyTowns = request.NearbyTowns,
            NotableLandMarks = request.NotableLandMarks,
            DistrictId = request.DistrictId,
            RegionId = request.RegionId
        };
    
    }
    
    public static Town MapToTown(this UpdateTownRequest request, Guid id)  //This maps the UpdateTownDto to Town
    {
        return new Town()
        {
            Id = id,
            TownName = request.TownName,
            Category = request.Category,
            Population =request.Population,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            StartDateTime = request.StartDateTime,
            LastModifiedDateTime  = request.LastModifiedDateTime,
            NearbyTowns = request.NearbyTowns,
            NotableLandMarks = request.NotableLandMarks,
            DistrictId = request.DistrictId,
            RegionId = request.RegionId
        };
    
    }
    
    
    public static TownResponse MapsToResponse(this Town town) //This maps the Town to TownResponseDto
    {
        return new TownResponse()
        {
            Id = town.Id,
            TownName = town.TownName,
            Category = town.Category,
            Population =town.Population,
            Latitude = town.Latitude,
            Longitude =town.Longitude,
            StartDateTime = town.StartDateTime,
            LastModifiedDateTime  = town.LastModifiedDateTime,
            NearbyTowns = town.NearbyTowns,
            NotableLandMarks = town.NotableLandMarks,
            DistrictId = town.DistrictId,
            RegionId = town.RegionId
        };
    }
    
    public static TownsResponse MapsToResponse(this IEnumerable<Town> regions) //This maps the list of Towns to the TownsResponses
    {
        return new TownsResponse()
        {
            Towns = regions.Select(MapsToResponse)
        };
    }
}