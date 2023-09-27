using AccraCity.Application.Models;
using AccraCityApi.Contracts.AccraCity;

namespace AccraCityApi.Services.Towns;

public interface ITownsService{

    void CreateTown(Town towns);
}