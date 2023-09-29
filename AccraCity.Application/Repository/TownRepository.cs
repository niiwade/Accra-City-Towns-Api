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
        var town = await _context.Town.ToListAsync(cancellationToken: token);
        return town;
    }

    public async Task<Town?> GetTownById(Guid id, CancellationToken token = default)
    {
        var result = await _context.Town.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: 
            token);
        return result;
    }

    public async Task<bool> CreateTown(Town town, CancellationToken token = default)
    {
        var newTown = new Town()
        {
            Id = Guid.NewGuid(),
            TownName = town.TownName, 
            Category = town.Category,
            Population =town.Population,
            Latitude = town.Latitude,
            Longitude =town.Longitude,
            CreatedAt = town.CreatedAt,
            //LastModifiedAt  = town.LastModifiedAt,
            NearbyTowns = town.NearbyTowns,
            NotableLandMarks = town.NotableLandMarks,
            DistrictId = town.DistrictId,
            RegionId = town.RegionId
        };
        await _context.AddAsync(newTown, token);
        return await Save(token);
    }

    public async Task<bool> UpdateTown(Town town, CancellationToken token = default)
    {
        var result = await _context.Town.FirstOrDefaultAsync(t => 
            t.Id  == town.Id, cancellationToken: token);
        
        if (result != null)
        {
            result.Id = town.Id;
            result.TownName = town.TownName;
            result.Category = town.Category;
            result.Population =town.Population;
            result.Latitude = town.Latitude;
            result.Longitude =town.Longitude;
            //result.CreatedAt = town.CreatedAt;
            result.LastModifiedAt  = town.LastModifiedAt;
            result.NearbyTowns = town.NearbyTowns;
            result.NotableLandMarks = town.NotableLandMarks;
            result.DistrictId = town.DistrictId;
            result.RegionId = town.RegionId;
        }
        return await Save(token);
    }

    public async Task<bool> DeleteTown(Guid id, CancellationToken token = default)
    {
        var result = await _context.Town.FirstOrDefaultAsync(i => 
            i.Id == id, cancellationToken: token);
        
        if (result == null)
        {
            return false; // Town not found or already deleted
        }
        _context.Remove(result);
        return await Save(token);
    }

    public async Task<bool> TownExists(Guid id, CancellationToken token = default)
    {
        var town =  await _context.Town.AnyAsync(r => r.Id == id, cancellationToken: token);
        return town;
    }

    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved =  await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}