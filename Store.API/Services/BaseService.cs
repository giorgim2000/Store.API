using Microsoft.EntityFrameworkCore;
using Store.API.DataContext;
using Store.API.Entities;
using Store.API.Models;

namespace Store.API.Services
{
    public class BaseService : IBaseService
    {
        private readonly ApplicationContext _context;
        public BaseService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateBase(int cityId, BaseForUpdateDto baseDto)
        {
            var theBase = await _context.Bases.FirstOrDefaultAsync(b => b.Id == baseDto.Id);
            if(theBase == null)
                return false;
            else
            {
                if(!string.IsNullOrWhiteSpace(baseDto.BaseName))
                    theBase.BaseName = baseDto.BaseName;
                if(!string.IsNullOrWhiteSpace(baseDto.Adress))
                    theBase.Adress = baseDto.Adress;
                    
                theBase.CityId = cityId;
                return await SaveChangesAsync();
            }
        }

        public async Task<Base?> CreateBase(BaseForCreationDto baseDto, int cityId)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
                return null;

            var baseToCreate = new Base()
            {
                BaseName = baseDto.BaseName,
                CityId = cityId,
                Adress = baseDto.Adress
            };

            await _context.Bases.AddAsync(baseToCreate);
            await SaveChangesAsync();

            return baseToCreate;
        }

        public async Task<bool> DeleteBaseById(int id)
        {
            var result = await _context.Bases.Where(b => b.Id == id).FirstOrDefaultAsync();
            if(result != null)
            {
                _context.Bases.Remove(result);
                return await SaveChangesAsync();
            }else
                return false;
        }

        public async Task<BaseDto?> GetBaseById(int id)
        {
            var baseDto = await _context.Bases.Where(c => c.Id == id)
                .Include(i => i.City)
                .FirstOrDefaultAsync();

            if (baseDto == null)
                return null;
            else
                return new BaseDto()
                {
                    BaseName = baseDto.BaseName,
                    Adress = baseDto.Adress,
                    CityName = baseDto.City.CityName
                };
        }

        public async Task<BaseDto?> GetBaseByName(string name)
        {
            var result = await _context.Bases
                .Where(i => i.BaseName.Contains(name))
                .Include(i => i.City)
                .FirstOrDefaultAsync();

            if (result == null)
                return null;
            else
                return new BaseDto() 
                { 
                    BaseName = result.BaseName, 
                    Adress = result.Adress,
                    CityName = result.City.CityName
                };
        }

        public async Task<List<BaseDto>> GetBases(int pageSize, int pageNumber)
        {
            return await _context.Bases.AsNoTracking()
                .Include(i => i.City)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(b => new BaseDto()
                {
                    Id = b.Id,
                    BaseName = b.BaseName,
                    Adress = b.Adress,
                    CityName = b.City.CityName
                })
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
