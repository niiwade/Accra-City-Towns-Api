namespace AccraCityApi.Contracts.Response.RegionResponses;

public class RegionsResponse
{
    public required IEnumerable<RegionResponse> Regions { get; init; } = Enumerable.Empty<RegionResponse>();
}