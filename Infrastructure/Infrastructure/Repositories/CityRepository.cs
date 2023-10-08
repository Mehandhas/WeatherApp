using Domain.DomainModels;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TInfrastructure;

namespace Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherDBContext _context;

        public CityRepository(WeatherDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }
    }
}
