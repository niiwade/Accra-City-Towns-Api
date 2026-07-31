using AccraCity.Application.Database;
using AccraCity.Application.Interface;
using AccraCity.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AccraCity.Application.Repository;

public class DistrictRepository : IDistrictRepository
{
    private readonly AppDbContext _context;

    public DistrictRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<District>> GetDistrictAsync(CancellationToken token = default)
    {
        return await _context.Districts.AsNoTracking().ToListAsync(cancellationToken: token);
    }

    public async Task<District?> GetDistrictById(Guid id, CancellationToken token = default)
    {
        return await _context.Districts.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);
    }

    public async Task<bool> CreateDistrict(District district, CancellationToken token = default)
    {
        await _context.AddAsync(district, token);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> UpdateDistrict(District district, CancellationToken token = default)
    {
        var result = await _context.Districts.FirstOrDefaultAsync(d => d.Id == district.Id, cancellationToken: token);

        if (result == null)
        {
            return false;
        }

        result.DistrictName = district.DistrictName;
        result.RegionId = district.RegionId;
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> DeleteDistrict(Guid id, CancellationToken token = default)
    {
        var result = await _context.Districts.FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);

        if (result == null)
        {
            return false;
        }

        _context.Remove(result);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> DistrictExists(Guid id, CancellationToken token = default)
    {
        return await _context.Districts.AnyAsync(r => r.Id == id, cancellationToken: token);
    }

    public async Task<bool> DistrictExistsByName(string districtName, CancellationToken token = default)
    {
        return await _context.Districts.AnyAsync(
            r => r.DistrictName.ToLower() == districtName.ToLower(),
            cancellationToken: token);
    }
}
