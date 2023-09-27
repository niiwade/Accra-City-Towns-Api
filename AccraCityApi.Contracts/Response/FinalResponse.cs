namespace AccraCityApi.Contracts.Response;

public class FinalResponse<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}