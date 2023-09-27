using AccraCity.Application.Interface;
using AccraCityApi.ContractMappings;
using AccraCityApi.Contracts.Requests.DistrictRequests;
using AccraCityApi.Contracts.Response;
using AccraCityApi.Contracts.Response.DistrictResponse;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers;

[ApiController]
public class DistrictController : Controller
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictController(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }

    //GET all Districts
    [HttpGet(ApiEndpoints.District.GetAll)]
    public async Task<IActionResult> GetDistricts(CancellationToken token)
    {
        var district = await _districtRepository.GetDistrictAsync(token);
        var districtsResponse = new FinalResponse<DistrictsResponse>
        {
            StatusCode = 200,
            Message = "Districts retrieved successfully.",
            Data = district.MapsToResponse()
        };
        return Ok(districtsResponse);
    }
    
    //GET DistrictById
    [HttpGet(ApiEndpoints.District.GetDistrict)]
    public async Task<IActionResult> GetDistrict([FromRoute] Guid id, CancellationToken token)
    {
        var district = await _districtRepository.GetDistrictById(id, token);
        if (district == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "District not found."
            });
        }
        var districtResponse = new FinalResponse<DistrictResponse>
        {
            StatusCode = 200,
            Message = "District retrieved successfully.",
            Data = district.MapsToResponse()
        };
        return Ok(districtResponse);
    }
    
    //POST Create District
    [HttpPost(ApiEndpoints.District.Create)]
    public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictRequest request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "District data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        
        var mapToDistrict = request.MapToDistrict();
        await _districtRepository.CreateDistrict(mapToDistrict ?? throw new InvalidOperationException(), token);
        var districtResponse = new FinalResponse<DistrictResponse>
        {
            StatusCode = 201,
            Message = "District created successfully.",
            Data = mapToDistrict.MapsToResponse()
        };
        return CreatedAtAction(nameof(GetDistrict), new { id = mapToDistrict.Id }, districtResponse);
    }
    
    //UPDATE Update District Details
    [HttpPut(ApiEndpoints.District.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDistrictRequest request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "District data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        var mapToDistrict = request.MapToDistrict(id);
        var updatedDistrict = await _districtRepository.UpdateDistrict(mapToDistrict, token);
        if (updatedDistrict is false)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "District not found."
            });
        }
        var response = new FinalResponse<DistrictResponse>
        {
            StatusCode = 200,
            Message = "District details updated successfully.",
            Data = mapToDistrict.MapsToResponse()
        };
        return Ok(response);

    }
    
    //DELETE District 
    [HttpDelete(ApiEndpoints.District.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _districtRepository.DistrictExists(id, token);
        var deleteDistrict = await _districtRepository.DeleteDistrict(id, token);
        if (!deleteDistrict)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "District not found or already deleted",
                Data = null
            });
        }
        
        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "District deleted successfully",
            Data = null
        });
    }
}