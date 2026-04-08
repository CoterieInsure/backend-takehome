using Assessment.Api.Features.BusinessFactors.Models;

namespace Assessment.Api.Features.BusinessFactors.Services;

public interface IBusinessFactorService
{
    Task<IEnumerable<BusinessFactor>> GetAllAsync();
    Task<BusinessFactor?> GetByBusinessAsync(string business);
    Task<BusinessFactor> CreateAsync(BusinessFactor factor);
    Task<bool> UpdateAsync(string business, BusinessFactor factor);
    Task<bool> DeleteAsync(string business);
    Task<bool> ExistsAsync(string business);
}
