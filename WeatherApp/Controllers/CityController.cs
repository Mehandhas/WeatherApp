using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository) 
        {
            _cityRepository= cityRepository;
        }

        [HttpGet]
        [Route("/getallcities")]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var variables = await _cityRepository.GetCitiesAsync();
            return Ok(variables);
        }
    }
}
