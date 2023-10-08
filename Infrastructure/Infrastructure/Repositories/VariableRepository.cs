using Domain.DomainModels;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TInfrastructure;

namespace Infrastructure.Repositories
{
    public class VariableRepository : IVariableRepository
    {
        private readonly WeatherDBContext _context;
        public VariableRepository(WeatherDBContext context)
        {
            _context = context;
        }
        public async Task<List<TemperatureTrend>> GetVariablesAsync(string variableName, DateTimeOffset startTimestamp, DateTimeOffset endTimestamp, int cityId, string? unit)
        {
            var variables = await (from variable in _context.Variables
                                   join city in _context.Cities on variable.CityId equals city.Id
                                   where variable.Name == variableName
                                       && variable.Timestamp >= startTimestamp
                                       && variable.Timestamp <= endTimestamp
                                       && (string.IsNullOrEmpty(unit) || variable.Unit == "°" + unit)
                                       && variable.CityId == cityId

                                   select new TemperatureTrend
                                   {
                                       VariableName = variable.Name,
                                       variableUnit = variable.Unit,
                                       VariableValue = Convert.ToDecimal(variable.Value),
                                       VariableTimestamp = variable.Timestamp,
                                       CityName = city.CityName
                                   }).ToListAsync();
            return variables;
        }

        public async Task<HottestCity?> GetHottestCityAsync()
        {

            var variable = await _context.Variables
                .Include(x => x.City)
                 .Where(x => x.Timestamp.Year == 2023
                 && x.Timestamp.Month == 1
                 && x.Name == "Temperature"
                 && x.Unit == "°C"
                 && Convert.ToDecimal(x.Value) > 30)
                .GroupBy(x => x.CityId)
                 .Select(group => new
                 {
                     CityName = group.Max(x => x.City.CityName),
                     TotalDaysAbove30C = group.Count()
                 })
                .OrderByDescending(x => x.TotalDaysAbove30C)
                .FirstOrDefaultAsync();
            if (variable == null)
            {
                return null;
            }

            return new HottestCity { CityName = variable.CityName, TotalDaysAbove30C = variable.TotalDaysAbove30C };
        }

        public async Task<MoistestCity?> GetMoistestCityAsync()
        {

            var variable = await _context.Variables
                .Include(x => x.City)
                 .Where(x => x.Timestamp.Year == 2023
                 && x.Timestamp.Month == 1
                 && x.Name == "Humidity")
                .GroupBy(x => x.CityId)
                 .Select(group => new
                 {
                     CityName = group.Max(x => x.City.CityName),
                     AverageHumidity = group.Average(h => Convert.ToDecimal(h.Value))
                 })
                .OrderByDescending(x => x.AverageHumidity)
                .FirstOrDefaultAsync();
            if (variable == null)
            {
                return null;
            }

            return new MoistestCity { CityName = variable.CityName, AverageHumidity = variable.AverageHumidity };
        }
    }
}
