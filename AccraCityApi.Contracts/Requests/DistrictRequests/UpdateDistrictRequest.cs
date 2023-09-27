namespace AccraCityApi.Contracts.Requests.DistrictRequests;

public class UpdateDistrictRequest
{
    public required string DistrictName { get; set; }
    public Guid RegionId { get; set; }
}