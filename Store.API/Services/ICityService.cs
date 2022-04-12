using Store.API.Entities;
using Store.API.Models;

namespace Store.API.Services
{
    public interface ICityService
    {
        Task<List<CityDto>> GetAllCities(int pageSize, int pageNumber);
        Task<CityDto?> GetCityById(int id);
        Task<City?> CreateCityAsync(CityDto city);
        Task<bool> CityExistsAsync(int cityId);
        Task<bool> ChangeCityName(int id, string cityName);
        Task<bool> DeleteCityById(int id);
        Task<bool> SaveChangesAsync();
    }
}
