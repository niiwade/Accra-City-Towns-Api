using AccraCity.Application.Models;
using AccraCityApi.Contracts.Requests.DistrictRequests;
using AccraCityApi.Contracts.Response.DistrictResponse;

namespace AccraCityApi.ContractMappings;

public static class DistrictContractMapping
{
    public static District MapToDistrict(this CreateDistrictRequest request)  //This maps the CreateDistrictDto to Region
    {
        return new District()
        {
            Id = Guid.NewGuid(),
            DistrictName = request.DistrictName,
            RegionId = request.RegionId
        };
    
    }
    
    public static District MapToDistrict(this UpdateDistrictRequest request, Guid id)  //This maps the UpdateDistrictDto to Region
    {
        return new District()
        {
            Id = id,
            DistrictName = request.DistrictName,
            RegionId = request.RegionId
        };
    
    }
    
    
    public static DistrictResponse MapsToResponse(this District district) //This maps the District to DistrictResponseDto
    {
        return new DistrictResponse()
        {
            Id = district.Id,
            DistrictName = district.DistrictName,
            RegionId = district.RegionId
        };
    }
    
    public static DistrictsResponse MapsToResponse(this IEnumerable<District> districts) //This maps the list of Districts to the DistrictsResponses
    {
        return new DistrictsResponse()
        {
            Districts = districts.Select(MapsToResponse)
        };
    }
}