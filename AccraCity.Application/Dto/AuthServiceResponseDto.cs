namespace AccraCity.Application.Dto;

public class AuthServiceResponseDto
{
    public int? StatusCode { get; set; }
    public bool IsSucceed { get; set; }
    public string? Message { get; set; }
    public string? Data { get; set; }
}