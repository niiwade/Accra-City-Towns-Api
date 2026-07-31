using AccraCity.Application.Database;
using AccraCity.Application.Interface;
using AccraCity.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AccraCity.Application.Repository;

public class RegionRepository : IRegionRepository
{
    private readonly AppDbContext _context;

    public RegionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Region>> GetRegionAsync(CancellationToken token = default)
    {
        return await _context.Regions.AsNoTracking().ToListAsync(cancellationToken: token);
    }

    public async Task<Region?> GetRegionById(Guid id, CancellationToken token = default)
    {
        return await _context.Regions.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);
    }

    public async Task<bool> CreateRegion(Region region, CancellationToken token = default)
    {
        await _context.AddAsync(region, token);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> UpdateRegion(Region region, CancellationToken token = default)
    {
        var result = await _context.Regions.FirstOrDefaultAsync(r => r.Id == region.Id, cancellationToken: token);

        if (result == null)
        {
            return false;
        }

        result.RegionName = region.RegionName;
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> DeleteRegion(Guid id, CancellationToken token = default)
    {
        var result = await _context.Regions.FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);

        if (result == null)
        {
            return false;
        }

        _context.Remove(result);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> RegionExists(Guid id, CancellationToken token = default)
    {
        return await _context.Regions.AnyAsync(r => r.Id == id, cancellationToken: token);
    }

    public async Task<bool> RegionExistsByName(string regionName, CancellationToken token = default)
    {
        return await _context.Regions.AnyAsync(
            r => r.RegionName.ToLower() == regionName.ToLower(),
            cancellationToken: token);
    }
}
