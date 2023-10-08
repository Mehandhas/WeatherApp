using Domain.DomainModels;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City?>> GetCitiesAsync();
    }
}
