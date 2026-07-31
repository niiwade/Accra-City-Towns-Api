using AccraCity.Application.Database;
using AccraCity.Application.Interface;
using AccraCity.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AccraCity.Application.Repository;

public class TownRepository : ITownRepository
{
    private readonly AppDbContext _context;

    public TownRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Town>> GetTownAsync(CancellationToken token = default)
    {
        return await _context.Town.AsNoTracking().ToListAsync(cancellationToken: token);
    }

    public async Task<Town?> GetTownById(Guid id, CancellationToken token = default)
    {
        return await _context.Town.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken: token);
    }

    public async Task<bool> CreateTown(Town town, CancellationToken token = default)
    {
        await _context.AddAsync(town, token);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> UpdateTown(Town town, CancellationToken token = default)
    {
        var result = await _context.Town.FirstOrDefaultAsync(t => t.Id == town.Id, cancellationToken: token);

        if (result == null)
        {
            return false;
        }

        result.TownName = town.TownName;
        result.Category = town.Category;
        result.Population = town.Population;
        result.Latitude = town.Latitude;
        result.Longitude = town.Longitude;
        result.LastModifiedAt = DateTime.UtcNow;
        result.NearbyTowns = town.NearbyTowns;
        result.NotableLandMarks = town.NotableLandMarks;
        result.DistrictId = town.DistrictId;
        result.RegionId = town.RegionId;

        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> DeleteTown(Guid id, CancellationToken token = default)
    {
        var result = await _context.Town.FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);

        if (result == null)
        {
            return false;
        }

        _context.Remove(result);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> TownExists(Guid id, CancellationToken token = default)
    {
        return await _context.Town.AnyAsync(r => r.Id == id, cancellationToken: token);
    }

    public async Task<bool> TownExistsByName(string townName, CancellationToken token = default)
    {
        return await _context.Town.AnyAsync(
            r => r.TownName.ToLower() == townName.ToLower(),
            cancellationToken: token);
    }
}
