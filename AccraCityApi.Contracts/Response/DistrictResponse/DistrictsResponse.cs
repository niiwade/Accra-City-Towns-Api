namespace AccraCityApi.Contracts.Response.DistrictResponse;

public class DistrictsResponse
{
    public required IEnumerable<DistrictResponse> Districts { get; init; } = Enumerable.Empty<DistrictResponse>();
}