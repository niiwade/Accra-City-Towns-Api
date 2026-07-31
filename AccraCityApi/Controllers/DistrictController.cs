using AccraCity.Application.Interface;
using AccraCityApi.ContractMappings;
using AccraCityApi.Contracts.Requests.DistrictRequests;
using AccraCityApi.Contracts.Response;
using AccraCityApi.Contracts.Response.DistrictResponse;
using Microsoft.AspNetCore.Mvc;

namespace AccraCityApi.Controllers;

[ApiController]
public class DistrictController : ControllerBase
{
    private readonly IDistrictRepository _districtRepository;
    private readonly ILogger<DistrictController> _logger;

    public DistrictController(IDistrictRepository districtRepository, ILogger<DistrictController> logger)
    {
        _districtRepository = districtRepository;
        _logger = logger;
    }

    [HttpGet(ApiEndpoints.District.GetAll)]
    public async Task<IActionResult> GetDistricts(CancellationToken token)
    {
        _logger.LogInformation("Get all districts method executing");
        var district = await _districtRepository.GetDistrictAsync(token);
        var districtsResponse = new FinalResponse<DistrictsResponse>
        {
            StatusCode = 200,
            Message = "Districts retrieved successfully.",
            Data = district.MapsToResponse()
        };
        _logger.LogInformation("Get all districts method successful");
        return Ok(districtsResponse);
    }

    [HttpGet(ApiEndpoints.District.Get)]
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
        _logger.LogInformation("GetDistrict method successful");
        return Ok(districtResponse);
    }

    [HttpPost(ApiEndpoints.District.Create)]
    public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictRequest request, CancellationToken token)
    {
        if (await _districtRepository.DistrictExistsByName(request.DistrictName, token))
        {
            return Conflict(new FinalResponse<object> { StatusCode = 409, Message = "District already exists." });
        }

        var mapToDistrict = request.MapToDistrict();
        _logger.LogInformation("CreateDistrict method executing");
        await _districtRepository.CreateDistrict(mapToDistrict, token);

        var districtResponse = new FinalResponse<DistrictResponse>
        {
            StatusCode = 201,
            Message = "District created successfully.",
            Data = mapToDistrict.MapsToResponse()
        };
        _logger.LogInformation("CreateDistrict method successful");
        return CreatedAtAction(nameof(GetDistrict), new { id = mapToDistrict.Id }, districtResponse);
    }

    [HttpPut(ApiEndpoints.District.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDistrictRequest request, CancellationToken token)
    {
        var mapToDistrict = request.MapToDistrict(id);
        _logger.LogInformation("UpdateDistrict method executing");

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
        _logger.LogInformation("UpdateDistrict method successful");
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.District.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        _logger.LogInformation("DeleteDistrict method executing");
        var deleteDistrict = await _districtRepository.DeleteDistrict(id, token);
        if (!deleteDistrict)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "District not found or already deleted"
            });
        }

        _logger.LogInformation("DeleteDistrict method successful");
        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "District deleted successfully"
        });
    }
}
