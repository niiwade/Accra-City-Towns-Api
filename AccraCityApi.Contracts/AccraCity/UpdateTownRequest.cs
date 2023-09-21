namespace AccraCityApi.Contracts.AccraCity;


public record UpdateTownRequest(
    string TownName,
    string District,
    string Category,
    string Region,
    int Population,
    float Latitude,
    float Longtitude,
    DateTime startDateTime,
    List<string> NearbyTowns,
    List<string> NotableLandMarks
);
