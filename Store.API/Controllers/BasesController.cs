using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Models;
using Store.API.Services;

namespace Store.API.Controllers
{
    [Route("api/bases")]
    [ApiController]
    public class BasesController : ControllerBase
    {
        private readonly IBaseService _baseService;
        const int maxPageSize = 20;
        public BasesController(IBaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BaseDto>>> GetBases(int pageSize = 10, int pageNumber = 1)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            return Ok(await _baseService.GetBases(pageSize, pageNumber));
        }
        [HttpGet("{baseId}", Name ="GetBase")]
        public async Task<ActionResult<BaseDto?>> GetBaseById(int baseId)
        {
            var baseResult = await _baseService.GetBaseById(baseId);
            if (baseResult == null)
                return NotFound();
            else
                return baseResult;
        }

        [HttpGet(nameof(GetBaseByName))]
        public async Task<ActionResult<BaseDto?>> GetBaseByName(string name)
        {
            var baseResult = await _baseService.GetBaseByName(name);
            if (baseResult == null)
                return NotFound();
            else
                return baseResult;
        }
        [HttpPost]
        public async Task<ActionResult<BaseForCreationDto?>> CreateBase(BaseForCreationDto baseDto, int cityId)
        {
            var res = await _baseService.CreateBase(baseDto, cityId);
            if (res == null)
                return NotFound();
            else
                return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> ChangeBaseName(int cityId, BaseForUpdateDto baseDto)
        {
            var res = await _baseService.UpdateBase(cityId, baseDto);
            if (res == false)
                return NotFound();
            else
                return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBase(int baseId)
        {
            var res = await _baseService.DeleteBaseById(baseId);
            if(res)
                return NoContent();
            else
                return NotFound();
        }
    }
}
