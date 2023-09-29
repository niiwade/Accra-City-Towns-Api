using AccraCity.Application.Models;

namespace AccraCity.Application.Interface;

public interface IDistrictRepository
{
    Task<IEnumerable<District>> GetDistrictAsync(CancellationToken token = default);
    Task<District?> GetDistrictById(Guid id, CancellationToken token = default);
    Task<bool> CreateDistrict(District district, CancellationToken token = default);
    Task<bool> UpdateDistrict(District district, CancellationToken token = default);
    Task<bool> DeleteDistrict(Guid id, CancellationToken token = default);
    Task<bool> DistrictExists(Guid id, CancellationToken token = default);
    Task<bool> DistrictExistsByName(string districtName, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}