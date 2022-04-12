using Store.API.Entities;
using Store.API.Models;

namespace Store.API.Services
{
    public interface IBaseService
    {
        Task<List<BaseDto>> GetBases(int pageSize, int pageNumber);
        Task<BaseDto?> GetBaseById(int id);
        Task<BaseDto?> GetBaseByName(string name);
        Task<Base?> CreateBase(BaseForCreationDto baseDto, int cityId);
        Task<bool> UpdateBase(int cityId, BaseForUpdateDto baseDto);
        Task<bool> DeleteBaseById(int id);
        Task<bool> SaveChangesAsync();
    }
}
