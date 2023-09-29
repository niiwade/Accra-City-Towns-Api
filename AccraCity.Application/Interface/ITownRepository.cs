using AccraCity.Application.Models;

namespace AccraCity.Application.Interface;

public interface ITownRepository
{
    Task<IEnumerable<Town>> GetTownAsync(CancellationToken token = default);
    Task<Town?> GetTownById(Guid id, CancellationToken token = default);
    Task<bool> CreateTown(Town town, CancellationToken token = default);
    Task<bool> UpdateTown(Town town, CancellationToken token = default);
    Task<bool> DeleteTown(Guid id, CancellationToken token = default);
    Task<bool> TownExists(Guid id, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}