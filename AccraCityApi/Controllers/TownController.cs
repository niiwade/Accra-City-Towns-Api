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
    private readonly ILogger<TownController> _logger;


    public TownController(ITownRepository townRepository, ILogger<TownController> logger)
    {
        _townRepository = townRepository;
        _logger = logger;
    }

    //GET all Towns
    [HttpGet(ApiEndpoints.Town.GetAll)]
    public async Task<IActionResult> GetTowns(CancellationToken token)
    {
        try
        {
            _logger.LogInformation("Get All towns method executing");
            var towns = await _townRepository.GetTownAsync(token);
            var townsResponse = new FinalResponse<TownsResponse>
            {
                StatusCode = 200,
                Message = "Towns retrieved successfully.",
                Data = towns.MapsToResponse()
            };
            _logger.LogInformation("Get All towns method successful");
            return Ok(townsResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Get Towns Method");
            _logger.LogError(ex, "Error in Get Towns Method");
            return Ok();
        }
    }
    
    //GET TownById
    [HttpGet(ApiEndpoints.Town.GetTown)]
    public async Task<IActionResult> GetTown([FromRoute] Guid id, CancellationToken token)
    {
        try
        {
            _logger.LogInformation("GetTown method executing");
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
            _logger.LogInformation("GetTown method : success");
            return Ok(townResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Get Town Method");
            _logger.LogError(ex, "Error in Get Town Method");
            return Ok();
        }
    }
    
    //POST Create Town
    [HttpPost(ApiEndpoints.Town.Create)]
    public async Task<IActionResult> CreateTown([FromBody] CreateTownRequest request, CancellationToken token)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Town data is invalid." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
            }

            var townExists = _townRepository.TownExistsByName(request.TownName, token);
            if (await townExists)
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
            _logger.LogInformation("CreateTown method success");
            return CreatedAtAction(nameof(GetTown), new { id = mapToTown.Id }, townResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Create Town Method");
            _logger.LogError(ex, "Error in Create Town Method");
            return Ok();
        }
        
    }
    
    //UPDATE Update Town Details
    [HttpPut(ApiEndpoints.Town.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTownRequest request, CancellationToken token)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Town data is invalid." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
            }

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
            _logger.LogInformation("UpdateTown method success");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Update Town Method");
            _logger.LogError(ex, "Error in Update Town Method");
            return Ok();
        }
    }
    
    //DELETE Town 
    [HttpDelete(ApiEndpoints.Town.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        try
        {
            await _townRepository.TownExists(id, token);
            _logger.LogInformation("DeleteTown method executing");
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

            _logger.LogInformation("DeleteTown method success");
            return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "Town deleted successfully",
                Data = null
            });
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Delete Town Method");
            _logger.LogError(ex, "Error in Delete Town Method");
            return Ok();
        }
    }
}