using AccraCityApi.Contracts.AccraCity;
using AccraCityApi.Controllers;
using AccraCityApi.Models;
using AccraCityApi.Services.Towns;
using Microsoft.AspNetCore.Mvc;


namespace AccraCityApi.Controllers;

[ApiController]//api controller attribute

[Route("[controller]")]
public class TownController : ControllerBase
{

    private readonly ITownsService _townService;

    public TownController(ITownsService townsService)
    {
        _townService = townsService;
    }


    [HttpPost]
    public IActionResult CreateTown(CreateTownRequest request)
    {
        //map request to Town object
        var town = new Town(
            Guid.NewGuid(),
            request.TownName,
            request.District,
            request.Category,
            request.Region,
            request.Population,
            request.Latitude,
            request.Longtitude,
            request.startDateTime,
            DateTime.UtcNow,
            request.NearbyTowns,
            request.NotableLandMarks
        );

        //TODO: save request to db
        _townService.CreateTown(town);


        var response = new TownResponse( // convert data to api definition
            town.Id,
            town.TownName,
            town.District,
            town.Category,
            town.Region,
            town.Population,
            town.Latitude,
            town.Longtitude,
            town.StartDateTime,
            town.LastModifiedDateTime,
            town.NearbyTowns,
            town.NotableLandMarks
        );

        return CreatedAtAction(
            actionName: nameof(GetTown),
            routeValues: new { id = town.Id },
            (response));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetTown(Guid id)
    {
        return Ok(id);
    }


    [HttpPut("{id:guid}")]
    public IActionResult UpdateTown(Guid id, UpdateTownRequest request)
    {
        return Ok(request);
    }


    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTown(Guid id)
    {
        return Ok(id);
    }



}