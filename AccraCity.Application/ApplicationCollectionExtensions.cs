using AccraCity.Application.Interface;
using AccraCity.Application.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AccraCity.Application;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IRegionRepository, RegionRepository>();
        service.AddScoped<IDistrictRepository, DistrictRepository>();
        service.AddScoped<ITownRepository, TownRepository>();
        return service;
    }

}