namespace AccraCityApi.Contracts.Requests.DistrictRequests;

public class CreateDistrictRequest
{
    public required string DistrictName { get; set; }
    public Guid RegionId { get; set; }
}