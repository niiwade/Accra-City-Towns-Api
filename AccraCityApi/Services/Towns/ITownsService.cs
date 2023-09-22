using AccraCityApi.Contracts.AccraCity;
using AccraCityApi.Models;

namespace AccraCityApi.Services.Towns;

public interface ITownsService{

    void CreateTown(Town towns);
}