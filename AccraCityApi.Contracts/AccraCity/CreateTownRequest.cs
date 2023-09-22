namespace AccraCityApi.Contracts.AccraCity;


public record CreateTownRequest(
     string TownName,
    string District,
    string Category,
    string Region,
    int Population,
    float Latitude,
    float Longtitude,
    DateTime startDateTime,
    DateTime lastModifiedDateTime,
    List<string> NearbyTowns,
    List<string> NotableLandMarks
);
