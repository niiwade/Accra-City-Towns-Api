using AccraCity.Application.Dto;

namespace AccraCity.Application.Interface;

public interface IAuthRepository
{
    Task<AuthServiceResponseDto> SeedRolesAsync();
    Task<AuthServiceResponseDto> RegisterAsync (RegisterDto registerDto);
    Task<AuthServiceResponseDto> LoginAsync (LoginDto loginDto);
    Task<AuthServiceResponseDto> MakeAdminAsync (UpdatePermissionDto updatePermissionDto);
    Task<AuthServiceResponseDto> MakeOwnerAsync (UpdatePermissionDto updatePermissionDto);
    Task<AuthServiceResponseDto> RemoveAdminRoleAsync (UpdatePermissionDto updatePermissionDto);
    Task<AuthServiceResponseDto> RemoveOwnerRoleAsync (UpdatePermissionDto updatePermissionDto);
}