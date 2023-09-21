using AccraCityApi.Contracts.AccraCity;
using Microsoft.AspNetCore.Mvc;


namespace AccraCityApi.Controllers;

[ApiController]//api controller attribute

[Route("[controller]")]
public class TownController : ControllerBase
{

    [HttpPost("/town")]
    public IActionResult CreateTown(CreateTownRequest request)
    {
        return Ok(request);
    }

    [HttpGet("/town/{id:guid}")]
    public IActionResult GetTown(Guid id)
    {
        return Ok(id);
    }


    [HttpPut("/town/{id:guid}")]
    public IActionResult UpdateTown(Guid id, UpdateTownRequest request)
    {
        return Ok(request);
    }


    [HttpDelete("/town/{id:guid}")] 
    public IActionResult DeleteTown(Guid id)
    {
        return Ok(id);
    }



}