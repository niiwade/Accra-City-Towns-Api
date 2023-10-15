using AccraCity.Application.Interface;
using AccraCity.Application.Models;
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
    private readonly ILogger<DistrictController> _logger;

    public DistrictController(IDistrictRepository districtRepository, ILogger<DistrictController> logger)
    {
        _districtRepository = districtRepository;
        _logger = logger;
    }

    //GET all Districts
    [HttpGet(ApiEndpoints.District.GetAll)]
    public async Task<IActionResult> GetDistricts(CancellationToken token)
    {
        try
        {
            _logger.LogInformation("Get All Districts method executing");
            var district = await _districtRepository.GetDistrictAsync(token);
            var districtsResponse = new FinalResponse<DistrictsResponse>
            {
                StatusCode = 200,
                Message = "Districts retrieved successfully.",
                Data = district.MapsToResponse()
            };
            _logger.LogInformation("Get All Districts method success");
            return Ok(districtsResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Get Districts Method");
            _logger.LogError(ex, "Error in Get Districts Method");
            return Ok();
        }
    }
    
    //GET DistrictById
    [HttpGet(ApiEndpoints.District.GetDistrict)]
    public async Task<IActionResult> GetDistrict([FromRoute] Guid id, CancellationToken token)
    {
        try
        {
            _logger.LogInformation("GetDistrict method executing");
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
            _logger.LogInformation("GetDistrict method success");
            return Ok(districtResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in Get District Method");
            _logger.LogError(ex, "Error in Get District Method");
            return Ok();
        }
    }
    
    //POST Create District
    [HttpPost(ApiEndpoints.District.Create)]
    public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictRequest request, CancellationToken token)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "District data is invalid." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
            }

            var doesDistrictExists = _districtRepository.DistrictExistsByName(request.DistrictName, token);
            if (await doesDistrictExists)
            {
                return Conflict(new FinalResponse<object> { StatusCode = 409, Message = "District already exists." });
            }

            var mapToDistrict = request.MapToDistrict();

            _logger.LogInformation("CreateRegion method executing");
            await _districtRepository.CreateDistrict(mapToDistrict, token);
            var districtResponse = new FinalResponse<DistrictResponse>
            {
                StatusCode = 201,
                Message = "District created successfully.",
                Data = mapToDistrict.MapsToResponse()
            };
            _logger.LogInformation("CreateRegion method success");
            return CreatedAtAction(nameof(GetDistrict), new { id = mapToDistrict.Id }, districtResponse);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("CreateDistrict Method");
            _logger.LogError(ex, "Error in Get District Method");
            return Ok();
        }
    }
    
    //UPDATE Update District Details
    [HttpPut(ApiEndpoints.District.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDistrictRequest request, CancellationToken token)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "District data is invalid." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
            }

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
            _logger.LogInformation("UpdateDistrict method success");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in UpdateDistrict Method");
            _logger.LogError(ex, "Error in UpdateDistrict Method");
            return Ok();
        }

    }
    
    //DELETE District 
    [HttpDelete(ApiEndpoints.District.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        try
        {
            await _districtRepository.DistrictExists(id, token);

            _logger.LogInformation("DeleteDistrict method executing");
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

            _logger.LogInformation("DeleteDistrict method success");
            return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "District deleted successfully",
                Data = null
            });
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error in DeleteDistrict Method");
            _logger.LogError(ex, "Error in DeleteDistrict Method");
            return Ok();
        }
    }
}