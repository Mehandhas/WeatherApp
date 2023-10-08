using Domain.DomainModels;

namespace UnitTest.Infrastructure.TestData
{
    public static class TemperatureTestData
    {

        public static List<Variable> GetVariables()
        {
            return new List<Variable>
        {
            new Variable { Name = "Temperature", Timestamp = DateTimeOffset.UtcNow, CityId = 1, Unit = "°C", Value = "25" },
            new Variable { Name = "Humidity", Timestamp = DateTimeOffset.UtcNow, CityId = 2, Unit = "%", Value = "50" },
        };
        }

        public static List<Variable> GetHottestCityData()
        {
            return new List<Variable>
        {
            new Variable { Name = "Temperature", Timestamp = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero), CityId = 1, Unit = "°C", Value = "31" },
            new Variable { Name = "Temperature", Timestamp = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), CityId = 1, Unit = "°C", Value = "31" },
            new Variable { Name = "Temperature", Timestamp = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), CityId = 2, Unit = "°C", Value = "32" },
        };
        }

        public static List<Variable> GetMoistestCityData()
        {
            return new List<Variable>
        {
            new Variable { Name = "Humidity", Timestamp = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero), CityId = 1, Unit = "%", Value = "50" },
            new Variable { Name = "Humidity", Timestamp = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), CityId = 1, Unit = "%", Value = "60" },
            new Variable { Name = "Humidity", Timestamp = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero), CityId = 2, Unit = "%", Value = "70" },
            new Variable { Name = "Humidity", Timestamp = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), CityId = 2, Unit = "%", Value = "80"},
        };
        }

        public static List<City> GetCities()
        {
            return new List<City>
        {
            new City { CityName = "Singapore", Id = 1},
            new City { CityName = "Bangalore",  Id = 2},
        };
        }

    }
}
