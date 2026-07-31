using AccraCity.Application.Interface;
using AccraCityApi.ContractMappings;
using AccraCityApi.Contracts.Requests.RegionRequests;
using AccraCityApi.Contracts.Response;
using AccraCityApi.Contracts.Response.RegionResponses;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers;

[ApiController]
public class RegionController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;
    private readonly ILogger<RegionController> _logger;

    public RegionController(IRegionRepository regionRepository, ILogger<RegionController> logger)
    {
        _regionRepository = regionRepository;
        _logger = logger;
    }

    [HttpGet(ApiEndpoints.Region.GetAll)]
    public async Task<IActionResult> GetRegions(CancellationToken token)
    {
        _logger.LogInformation("Get all regions method executing");
        var region = await _regionRepository.GetRegionAsync(token);
        var regionsResponse = new FinalResponse<RegionsResponse>
        {
            StatusCode = 200,
            Message = "Regions retrieved successfully.",
            Data = region.MapsToResponse()
        };
        _logger.LogInformation("Get all regions method successful");
        return Ok(regionsResponse);
    }

    [HttpGet(ApiEndpoints.Region.Get)]
    public async Task<IActionResult> GetRegion([FromRoute] Guid id, CancellationToken token)
    {
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

    [HttpPost(ApiEndpoints.Region.Create)]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequest request, CancellationToken token)
    {
        if (await _regionRepository.RegionExistsByName(request.RegionName, token))
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

    [HttpPut(ApiEndpoints.Region.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest request, CancellationToken token)
    {
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
        _logger.LogInformation("UpdateRegion method successful");
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Region.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        _logger.LogInformation("DeleteRegion method executing");
        var deleteRegion = await _regionRepository.DeleteRegion(id, token);
        if (!deleteRegion)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Region not found or already deleted"
            });
        }

        _logger.LogInformation("DeleteRegion method successful");
        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "Region deleted successfully"
        });
    }
}
