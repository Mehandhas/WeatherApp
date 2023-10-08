using Infrastructure.Repositories;
using TInfrastructure;
using UnitTest.Infrastructure.TestData;

namespace UnitTest.Infrastructure.Repositories
{
    public class TemperatureTest
    {
        [Fact]
        public async Task GetVariablesAsync_ReturnsVariables()
        {
            // Arrange
            var options = DbContextOptionsFactory.Create<WeatherDBContext>();
            using var context = new WeatherDBContext(options);

            // Initialize test data
            var variableData = TemperatureTestData.GetVariables();
            var cityData = TemperatureTestData.GetCities();
            context.Variables.AddRange(variableData);
            context.Cities.AddRange(cityData);
            context.SaveChanges();
            var repository = new VariableRepository(context);

            // Act
            var result = await repository.GetVariablesAsync("Temperature", DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddDays(1), 1, "C");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Temperature", result[0].VariableName);
        }

        [Fact]
        public async Task GetHottestCityAsync_ReturnsHottestCity()
        {
            // Arrange
            var options = DbContextOptionsFactory.Create<WeatherDBContext>();
            using var context = new WeatherDBContext(options);

            // Initialize test data
            var variableData = TemperatureTestData.GetHottestCityData();
            var cityData = TemperatureTestData.GetCities();
            context.Variables.AddRange(variableData);
            context.Cities.AddRange(cityData);
            context.SaveChanges();
            var repository = new VariableRepository(context);

            // Act
            var result = await repository.GetHottestCityAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Singapore", result.CityName);
            Assert.Equal(2, result.TotalDaysAbove30C);
        }

        [Fact]
        public async Task GetMoistestCityAsync_ReturnsMoistestCity()
        {
            // Arrange
            var options = DbContextOptionsFactory.Create<WeatherDBContext>();
            using var context = new WeatherDBContext(options);

            // Initialize test data
            var variableData = TemperatureTestData.GetMoistestCityData();
            var cityData = TemperatureTestData.GetCities();
            context.Variables.AddRange(variableData);
            context.Cities.AddRange(cityData);
            context.SaveChanges();
            var repository = new VariableRepository(context);

            // Act
            var result = await repository.GetMoistestCityAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Bangalore", result.CityName); 
            Assert.Equal(75, result.AverageHumidity);
        }
    }
}