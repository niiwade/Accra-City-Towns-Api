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

    public RegionController(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }
    
    //GET all Regions
    [HttpGet(ApiEndpoints.Region.GetAll)]
    public async Task<IActionResult> GetRegions(CancellationToken token)
    {
        var region = await _regionRepository.GetRegionAsync(token);
        var regionsResponse = new FinalResponse<RegionsResponse>
        {
            StatusCode = 200,
            Message = "Regions retrieved successfully.",
            Data = region.MapsToResponse()
        };
        return Ok(regionsResponse);
    }
    
    //GET RegionById
    [HttpGet(ApiEndpoints.Region.GetRegion)]
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
        return Ok(regionResponse);
    }
    
    //POST Create Region
    [HttpPost(ApiEndpoints.Region.Create)]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequest request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Region data is invalid." });
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
        await _regionRepository.CreateRegion(mapToRegion, token);
        var regionResponse = new FinalResponse<RegionResponse>
        {
            StatusCode = 201,
            Message = "Region created successfully.",
            Data = mapToRegion.MapsToResponse()
        };
        return CreatedAtAction(nameof(GetRegion), new { id = mapToRegion.Id }, regionResponse);
    }
    
    //UPDATE Update Region Details
    [HttpPut(ApiEndpoints.Region.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Region data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        var mapToRegion = request.MapToRegion(id);
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
        return Ok(response);

    }

    //DELETE Region 
    [HttpDelete(ApiEndpoints.Region.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _regionRepository.RegionExists(id, token);
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
        
        return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "Region deleted successfully",
                Data = null
            });
    }
}