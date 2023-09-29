using AccraCity.Application.Interface;
using AccraCityApi.ContractMappings;
using AccraCityApi.Contracts.AccraCity;
using AccraCityApi.Contracts.Requests.TownRequests;
using AccraCityApi.Contracts.Response;
using AccraCityApi.Contracts.Response.TownResponses;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers;


[ApiController]
public class TownController : Controller
{
    private readonly ITownRepository _townRepository;

    public TownController(ITownRepository townRepository)
    {
        _townRepository = townRepository;
    }

    //GET all Towns
    [HttpGet(ApiEndpoints.Town.GetAll)]
    public async Task<IActionResult> GetTowns(CancellationToken token)
    {
        var towns = await _townRepository.GetTownAsync(token);
        var townsResponse = new FinalResponse<TownsResponse>
        {
            StatusCode = 200,
            Message = "Towns retrieved successfully.",
            Data = towns.MapsToResponse()
        };
        return Ok(townsResponse);
    }
    
    //GET TownById
    [HttpGet(ApiEndpoints.Town.GetTown)]
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
        return Ok(townResponse);
    }
    
    //POST Create Town
    [HttpPost(ApiEndpoints.Town.Create)]
    public async Task<IActionResult> CreateTown([FromBody] CreateTownRequest request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Town data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }

        var doesTownExists = _townRepository.TownExistsByName(request.TownName, token);
        if (await doesTownExists)
        {
            return Conflict(new FinalResponse<object> { StatusCode = 409, Message = "Town already exists." });
        }
        var mapToTown = request.MapToTown();
        await _townRepository.CreateTown(mapToTown, token);
        var townResponse = new FinalResponse<TownResponse>
        {
            StatusCode = 201,
            Message = "Town created successfully.",
            Data = mapToTown.MapsToResponse()
        };
        return CreatedAtAction(nameof(GetTown), new { id = mapToTown.Id }, townResponse);
        
    }
    
    //UPDATE Update Town Details
    [HttpPut(ApiEndpoints.Town.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTownRequest request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Town data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        var mapToTown = request.MapToTown(id);
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
        return Ok(response);
    }
    
    //DELETE Town 
    [HttpDelete(ApiEndpoints.Town.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _townRepository.TownExists(id, token);
        var deleteTown = await _townRepository.DeleteTown(id, token);
        if (!deleteTown)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Town not found or already deleted",
                Data = null
            });
        }
        
        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "Town deleted successfully",
            Data = null
        });
    }
}