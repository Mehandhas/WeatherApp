using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {

        private readonly IVariableRepository _variableRepository;
        public TemperatureController(IVariableRepository variableRepository)
        {
            _variableRepository = variableRepository;
        }

        [HttpGet]
        [Route("/temperaturetrend")]
        public async Task<IActionResult> GetVariablesAsync(string variableName, string startTimestamp, string endTimestamp, int cityId, string? unit)
        {
            var variables = await _variableRepository.GetVariablesAsync(variableName, Convert.ToDateTime(startTimestamp), Convert.ToDateTime(endTimestamp), cityId, unit);
            return Ok(variables);
        }

        [HttpGet]
        [Route("/hottestcity")]
        public async Task<IActionResult> GetHottestCityAsync()
        {
            var hottestCity = await _variableRepository.GetHottestCityAsync();
            return Ok(hottestCity);

        }

        [HttpGet]
        [Route("/moistestcity")]
        public async Task<IActionResult> GetMoistestCityAsync()
        {
            var moistestCity = await _variableRepository.GetMoistestCityAsync();
            return Ok(moistestCity);
        }
    }
}
