
namespace AccraCityApi.Models;


public class Town
{


    public Guid Id { get; }
    public string TownName { get; }
    public string District { get; }
    public string Category { get; }
    public string Region { get; }
    public int Population { get; }
    public float Latitude { get; }
    public float Longtitude { get; }
    public DateTime StartDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> NearbyTowns { get; }
    public List<string> NotableLandMarks { get; }


    public Town(
        Guid id,
        string town_Name,
        string district,
        string category,
        string region,
        int population,
        float latitude,
        float longtitude,
        DateTime startDateTime,
        DateTime lastModifiedDateTime,
        List<string> nearbyTowns,
        List<string> notableLandMarks
    )

    //enforce invariants
    {
        Id = id;
        TownName = town_Name;
        District = district;
        Category = category;
        Region = region;
        Population = population;
        Latitude = latitude;
        Longtitude = longtitude;
        StartDateTime = startDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        NearbyTowns = nearbyTowns;
        NotableLandMarks = notableLandMarks;
    }



}

