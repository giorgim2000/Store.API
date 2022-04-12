using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Entities;
using Store.API.Models;
using Store.API.Services;

namespace Store.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        const int maxPageSize = 20;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        public async Task<ActionResult<List<CityDto>>> GetAllCities(int pageSize = 10, int pageNumber = 1)
        {
            if(pageSize > maxPageSize)
                pageSize = maxPageSize;

            return Ok(await _cityService.GetAllCities(pageSize, pageNumber));
        }
        [HttpGet("/{cityId}", Name ="GetCity")]
        public async Task<ActionResult<CityDto>> GetCityById(int cityId)
        {
            var result = await _cityService.GetCityById(cityId);

            if(result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> CreateCity(string cityName)
        {
            var result = await _cityService.CreateCityAsync(new CityDto { CityName = cityName });
            if (result == null)
                return BadRequest();
            else
                return Created($"/{result.Id}", new CityDto() { Id=result.Id, CityName=result.CityName});
            
        }

        [HttpPut]
        public async Task<ActionResult> ChangeCityName(int cityId, string cityName)
        {
            if (await _cityService.GetCityById(cityId) == null)
                return NotFound();

            var result = await _cityService.ChangeCityName(cityId, cityName);
            if (result)
                return NoContent();
            else
                return BadRequest();
        }
        [HttpDelete("{cityId}")]
        public async Task<ActionResult> DeleteCity(int cityId)
        {
            var result = await _cityService.DeleteCityById(cityId);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
