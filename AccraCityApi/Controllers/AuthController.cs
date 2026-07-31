using AccraCity.Application.Dto;
using AccraCity.Application.Interface;
using AccraCity.Application.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;

        public AuthController(IAuthRepository authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiEndpoints.Auth.SeedRoles)]
        public async Task<IActionResult> SeedRoles()
        {
            var seedData = await _authService.SeedRolesAsync();
            return ToHttpResult(seedData);
        }

        [HttpPost(ApiEndpoints.Auth.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _authService.RegisterAsync(registerDto);
            return ToHttpResult(user);
        }

        [HttpPost(ApiEndpoints.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            var userLogin = await _authService.LoginAsync(loginRequest);
            return ToHttpResult(userLogin);
        }

        [Authorize(Roles = StaticUserRoles.ADMIN)]
        [HttpPost(ApiEndpoints.Auth.MakeAdmin)]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionRequest)
        {
            var user = await _authService.MakeAdminAsync(updatePermissionRequest);
            return ToHttpResult(user);
        }

        [Authorize(Roles = StaticUserRoles.OWNER)]
        [HttpPost(ApiEndpoints.Auth.MakeOwner)]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionRequest)
        {
            var user = await _authService.MakeOwnerAsync(updatePermissionRequest);
            return ToHttpResult(user);
        }

        [Authorize(Roles = StaticUserRoles.OWNER)]
        [HttpPost(ApiEndpoints.Auth.RemoveOwnerRole)]
        public async Task<IActionResult> RemoveOwnerRole([FromBody] UpdatePermissionDto updatePermissionRequest)
        {
            var user = await _authService.RemoveOwnerRoleAsync(updatePermissionRequest);
            return ToHttpResult(user);
        }

        [Authorize(Roles = StaticUserRoles.ADMIN)]
        [HttpPost(ApiEndpoints.Auth.RemoveAdminRole)]
        public async Task<IActionResult> RemoveAdminRole([FromBody] UpdatePermissionDto updatePermissionRequest)
        {
            var user = await _authService.RemoveAdminRoleAsync(updatePermissionRequest);
            return ToHttpResult(user);
        }

        private IActionResult ToHttpResult(AuthServiceResponseDto response) =>
            StatusCode(response.StatusCode ?? 400, response);
    }
}
