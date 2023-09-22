namespace AccraCityApi.Contracts.AccraCity;


public record TownResponse(
   Guid Id,
    string TownName,
    string District,
    string Category,
    string Region,
    int Population,
    float Latitude,
    float Longtitude,
    DateTime startDateTime,
    DateTime LastModifiedDateTime,
    List<string> NearbyTowns,
    List<string> NotableLandMarks
);
