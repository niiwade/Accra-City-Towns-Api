namespace AccraCityApi.Contracts.Response.DistrictResponse;

public class DistrictResponse
{
    public required Guid Id { get; set; }
    public required string DistrictName { get; set; }
    public Guid RegionId { get; set; }
}