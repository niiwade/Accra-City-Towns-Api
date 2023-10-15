using AccraCity.Application.Interface;
using AccraCityApi.ContractMappings;
using AccraCityApi.Contracts.Requests.RegionRequests;
using AccraCityApi.Contracts.Response;
using AccraCityApi.Contracts.Response.RegionResponses;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers;

[ApiController]
public class RegionController : Controller
{
    private readonly IRegionRepository _regionRepository;
    private readonly ILogger<RegionController> _logger;

    public RegionController(IRegionRepository regionRepository, ILogger<RegionController> logger)
    {
        _regionRepository = regionRepository;
        _logger = logger;
    }
    
    //GET all Regions
    [HttpGet(ApiEndpoints.Region.GetAll)]
    public async Task<IActionResult> GetRegions(CancellationToken token)
    {
        try
        {
            _logger.LogInformation("Get All Regions method executing");
            var region = await _regionRepository.GetRegionAsync(token);
            var regionsResponse = new FinalResponse<RegionsResponse>
            {
                StatusCode = 200,
                Message = "Regions retrieved successfully.",
                Data = region.MapsToResponse()
            };
            _logger.LogInformation("Get All Regions method successful");
            return Ok(regionsResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Get Regions Method");
            _logger.LogError(ex, "Error in Get Regions Method");
            return Ok();
        }
    }
    
    //GET RegionById
    [HttpGet(ApiEndpoints.Region.GetRegion)]
    public async Task<IActionResult> GetRegion([FromRoute] Guid id, CancellationToken token)
    {
        try
        {
            _logger.LogInformation("GetRegion method executing");
            var region = await _regionRepository.GetRegionById(id, token);
            if (region == null)
            {
                return NotFound(new FinalResponse<object>
                {
                    StatusCode = 404,
                    Message = "Region not found."
                });
            }

            var regionResponse = new FinalResponse<RegionResponse>
            {
                StatusCode = 200,
                Message = "Region retrieved successfully.",
                Data = region.MapsToResponse()
            };
            _logger.LogInformation("GetRegion method successful");
            return Ok(regionResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Get Region Method");
            _logger.LogError(ex, "Error in Get Region Method");
            return Ok();
        }
    }
    
    //POST Create Region
    [HttpPost(ApiEndpoints.Region.Create)]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequest request, CancellationToken token)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Region data is invalid." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
            }

            var doesRegionExists = _regionRepository.RegionExistsByName(request.RegionName, token);
            if (await doesRegionExists)
            {
                return Conflict(new FinalResponse<object> { StatusCode = 409, Message = "Region already exists." });
            }

            var mapToRegion = request.MapToRegion();
            _logger.LogInformation("CreateRegion method executing");
            await _regionRepository.CreateRegion(mapToRegion, token);
            var regionResponse = new FinalResponse<RegionResponse>
            {
                StatusCode = 201,
                Message = "Region created successfully.",
                Data = mapToRegion.MapsToResponse()
            };
            _logger.LogInformation("CreateRegion method successful");
            return CreatedAtAction(nameof(GetRegion), new { id = mapToRegion.Id }, regionResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Create Region Method");
            _logger.LogError(ex, "Error in Create Method");
            return Ok();
        }
    }
    
    //UPDATE Update Region Details
    [HttpPut(ApiEndpoints.Region.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest request, CancellationToken token)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Region data is invalid." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
            }

            var mapToRegion = request.MapToRegion(id);
            
            _logger.LogInformation("UpdateRegion method executing");
            var updatedRegion = await _regionRepository.UpdateRegion(mapToRegion, token);
            if (updatedRegion is false)
            {
                return NotFound(new FinalResponse<object>
                {
                    StatusCode = 404,
                    Message = "Region not found."
                });
            }

            var response = new FinalResponse<RegionResponse>
            {
                StatusCode = 200,
                Message = "Region details updated successfully.",
                Data = mapToRegion.MapsToResponse()
            };
            _logger.LogInformation("UpdateRegion method success");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Update Region Method");
            _logger.LogError(ex, "Error in Update Method");
            return Ok();
        }

    }

    //DELETE Region 
    [HttpDelete(ApiEndpoints.Region.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        try
        {
            await _regionRepository.RegionExists(id, token);
            _logger.LogInformation("DeleteRegion method executing");
            var deleteRegion = await _regionRepository.DeleteRegion(id, token);
            if (!deleteRegion)
            {
                return NotFound(new FinalResponse<string>
                {
                    StatusCode = 404,
                    Message = "Region not found or already deleted",
                    Data = null
                });
            }

            _logger.LogInformation("DeleteRegion method success");
            return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "Region deleted successfully",
                Data = null
            });
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Delete Region Method");
            _logger.LogError(ex, "Error in Delete Method");
            return Ok();
        }
    }
}