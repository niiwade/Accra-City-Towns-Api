using AccraCity.Application.Models;

namespace AccraCity.Application.Interface;

public interface IRegionRepository
{
    Task<IEnumerable<Region>> GetRegionAsync(CancellationToken token = default);
    Task<Region?> GetRegionById(Guid id, CancellationToken token = default);
    Task<bool> CreateRegion(Region region, CancellationToken token = default);
    Task<bool> UpdateRegion(Region region, CancellationToken token = default);
    Task<bool> DeleteRegion(Guid id, CancellationToken token = default);
    Task<bool> RegionExists(Guid id, CancellationToken token = default);
    Task<bool> RegionExistsByName(string regionName, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}