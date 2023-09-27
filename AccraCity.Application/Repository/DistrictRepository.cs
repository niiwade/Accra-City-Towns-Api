using System.Diagnostics;
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
        var district = await _context.Districts.ToListAsync(cancellationToken: token);
        return district;
    }

    public async Task<District?> GetDistrictById(Guid id, CancellationToken token = default)
    {
        var district = await _context.Districts.FirstOrDefaultAsync(i => i.Id == id, cancellationToken: token);
        return district;
    }

    public async Task<bool> CreateDistrict(District district, CancellationToken token = default)
    {
        var newDistrict = new District()
        {
            Id = district.Id,
            DistrictName = district.DistrictName,
            RegionId = district.RegionId
        };
        await _context.AddAsync(newDistrict, token);
        return await Save(token);
    }

    public async Task<bool> UpdateDistrict(District district, CancellationToken token = default)
    {
        var result = await _context.Districts.FirstOrDefaultAsync(d => 
            d.Id  == district.Id, cancellationToken: token);
        
        if (result != null)
        {
            result.DistrictName = district.DistrictName;
            result.RegionId = district.RegionId;
        }
        return await Save(token);
    }

    public async Task<bool> DeleteDistrict(Guid id, CancellationToken token = default)
    {
        var result = await _context.Districts.FirstOrDefaultAsync(i => 
            i.Id == id, cancellationToken: token);
        
        if (result == null)
        {
            return false; // District not found or already deleted
        }
        _context.Remove(result);
        return await Save(token);
    }

    public async Task<bool> DistrictExists(Guid id, CancellationToken token = default)
    {
        var district =  await _context.Districts.AnyAsync(r => r.Id == id, cancellationToken: token);
        return district;
    }

    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved =  await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}