using AccraCity.Application.Interface;
using AccraCityApi.ContractMappings;
using AccraCityApi.Contracts.AccraCity;
using AccraCityApi.Contracts.Requests.TownRequests;
using AccraCityApi.Contracts.Response;
using AccraCityApi.Contracts.Response.TownResponses;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers;

[ApiController]
public class TownController : ControllerBase
{
    private readonly ITownRepository _townRepository;
    private readonly ILogger<TownController> _logger;

    public TownController(ITownRepository townRepository, ILogger<TownController> logger)
    {
        _townRepository = townRepository;
        _logger = logger;
    }

    [HttpGet(ApiEndpoints.Town.GetAll)]
    public async Task<IActionResult> GetTowns(CancellationToken token)
    {
        _logger.LogInformation("Get all towns method executing");
        var towns = await _townRepository.GetTownAsync(token);
        var townsResponse = new FinalResponse<TownsResponse>
        {
            StatusCode = 200,
            Message = "Towns retrieved successfully.",
            Data = towns.MapsToResponse()
        };
        _logger.LogInformation("Get all towns method successful");
        return Ok(townsResponse);
    }

    [HttpGet(ApiEndpoints.Town.Get)]
    public async Task<IActionResult> GetTown([FromRoute] Guid id, CancellationToken token)
    {
        var town = await _townRepository.GetTownById(id, token);
        if (town == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "Town not found."
            });
        }

        var townResponse = new FinalResponse<TownResponse>
        {
            StatusCode = 200,
            Message = "Town retrieved successfully.",
            Data = town.MapsToResponse()
        };
        _logger.LogInformation("GetTown method successful");
        return Ok(townResponse);
    }

    [HttpPost(ApiEndpoints.Town.Create)]
    public async Task<IActionResult> CreateTown([FromBody] CreateTownRequest request, CancellationToken token)
    {
        if (await _townRepository.TownExistsByName(request.TownName, token))
        {
            return Conflict(new FinalResponse<object> { StatusCode = 409, Message = "Town already exists." });
        }

        var mapToTown = request.MapToTown();
        _logger.LogInformation("CreateTown method executing");
        await _townRepository.CreateTown(mapToTown, token);

        var townResponse = new FinalResponse<TownResponse>
        {
            StatusCode = 201,
            Message = "Town created successfully.",
            Data = mapToTown.MapsToResponse()
        };
        _logger.LogInformation("CreateTown method successful");
        return CreatedAtAction(nameof(GetTown), new { id = mapToTown.Id }, townResponse);
    }

    [HttpPut(ApiEndpoints.Town.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTownRequest request, CancellationToken token)
    {
        var mapToTown = request.MapToTown(id);
        _logger.LogInformation("UpdateTown method executing");

        var updatedTown = await _townRepository.UpdateTown(mapToTown, token);
        if (updatedTown is false)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "Town not found."
            });
        }

        var response = new FinalResponse<TownResponse>
        {
            StatusCode = 200,
            Message = "Town details updated successfully.",
            Data = mapToTown.MapsToResponse()
        };
        _logger.LogInformation("UpdateTown method successful");
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Town.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        _logger.LogInformation("DeleteTown method executing");
        var deleteTown = await _townRepository.DeleteTown(id, token);
        if (!deleteTown)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Town not found or already deleted"
            });
        }

        _logger.LogInformation("DeleteTown method successful");
        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "Town deleted successfully"
        });
    }
}
