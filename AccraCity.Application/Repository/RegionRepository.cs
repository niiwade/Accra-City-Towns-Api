using AccraCity.Application.Database;
using AccraCity.Application.Interface;
using AccraCity.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AccraCity.Application.Repository;

public class RegionRepository: IRegionRepository
{
    private readonly AppDbContext _context;

    public RegionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Region>> GetRegionAsync(CancellationToken token = default)
    {
        var region = await _context.Regions.ToListAsync(cancellationToken: token);
        return region;
    }

    public async Task<Region?> GetRegionById(Guid id, CancellationToken token = default)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);
        return region;
    }

    public async Task<bool> CreateRegion(Region region, CancellationToken token = default)
    {
        var newRegion = new Region()
        {
            Id = region.Id,
            RegionName = region.RegionName
        };
        await _context.AddAsync(newRegion, token);
        return await Save(token);
    }

    public async Task<bool> UpdateRegion(Region region, CancellationToken token = default)
    {
        var result = await _context.Regions.FirstOrDefaultAsync(r => 
            r.Id  == region.Id, cancellationToken: token);
        
        if (result != null)
        {
            result.RegionName = region.RegionName;
        }
        return await Save(token);
    }

    public async Task<bool> DeleteRegion(Guid id, CancellationToken token = default)
    {
        var result = await _context.Regions.FirstOrDefaultAsync(i => 
            i.Id == id, cancellationToken: token);
        
        if (result == null)
        {
            return false; // Region not found or already deleted
        }
        _context.Remove(result);
        return await Save(token);
    }

    public async Task<bool> RegionExists(Guid id, CancellationToken token = default)
    {
        var region =  await _context.Regions.AnyAsync(r => r.Id == id, cancellationToken: token);
        return region;
    }

    public async Task<bool> RegionExistsByName(string regionName, CancellationToken token = default)
    {
        var region =  await _context.Regions.AnyAsync(r => r.RegionName == regionName, cancellationToken: token);
        return region;
    }

    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved =  await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}