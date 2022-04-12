using Microsoft.EntityFrameworkCore;
using Store.API.DataContext;
using Store.API.Entities;
using Store.API.Models;

namespace Store.API.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationContext _context;
        public CityService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<CityDto>> GetAllCities(int pageSize, int pageNumber)
        {
            return await _context.Cities.AsNoTracking()
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(c => new CityDto() { Id=c.Id, CityName = c.CityName })
                .ToListAsync();
        }
        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<CityDto?> GetCityById(int id)
        {
            var city = await _context.Cities.Where(c => c.Id == id)
                                    .FirstOrDefaultAsync();
            if (city == null)
                return null;
            else
                return new CityDto()
                 {
                    Id = city.Id,
                    CityName = city.CityName
                 };
        }

        public async Task<City?> CreateCityAsync(CityDto city)
        {
            var cityToCreate = new City() { CityName= city.CityName };
            await _context.Cities.AddAsync(cityToCreate);
            var res = await SaveChangesAsync();
            if (res)
                return cityToCreate;
            else
                return null;
                    
        }
        public async Task<bool> ChangeCityName(int id, string cityName)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            if (city != null)
            {
                city.CityName = cityName;
                return await SaveChangesAsync();
            }
            else
                return false;


        }

        public async Task<bool> DeleteCityById(int id)
        {
            var city = await _context.Cities
                .Include(i => i.Bases)
                .FirstOrDefaultAsync(c => c.Id == id);
                
            if (city != null)
            {
                if (city.Bases.Count() == 0)
                {
                    _context.Remove(city);
                    return await SaveChangesAsync();
                }
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
