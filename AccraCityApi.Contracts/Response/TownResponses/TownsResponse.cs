namespace AccraCityApi.Contracts.Response.TownResponses;

public class TownsResponse
{
    public required IEnumerable<TownResponse> Towns { get; init; } = Enumerable.Empty<TownResponse>();
}