using Assessment.Api.Features.StateFactors.Models;

namespace Assessment.Api.Features.StateFactors.Services;

public interface IStateFactorService
{
    Task<IEnumerable<StateFactor>> GetAllAsync();
    Task<StateFactor?> GetByStateAsync(string state);
    Task<StateFactor> CreateAsync(StateFactor factor);
    Task<bool> UpdateAsync(string state, StateFactor factor);
    Task<bool> DeleteAsync(string state);
    Task<bool> ExistsAsync(string state);
}
