
using Domain.DomainModels;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IVariableRepository
    {
        Task<List<TemperatureTrend>> GetVariablesAsync(string variableName, DateTimeOffset startTimestamp, DateTimeOffset endTimestamp, int cityId, string? unit);
        Task<HottestCity?> GetHottestCityAsync();
        Task<MoistestCity?> GetMoistestCityAsync();
    }
}
