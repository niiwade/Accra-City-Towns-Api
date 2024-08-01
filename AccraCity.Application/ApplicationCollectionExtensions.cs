using AccraCity.Application.Interface;
using AccraCity.Application.Repository;
using AccraCity.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AccraCity.Application;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IRegionRepository, RegionRepository>();
        service.AddScoped<IDistrictRepository, DistrictRepository>();
        service.AddScoped<ITownRepository, TownRepository>();
        service.AddScoped<IAuthRepository, AuthService>();
        return service;
    }

}