namespace Assessment.Api.Features.CarrierEligibility.Services;

public interface ICarrierEligibilityService
{
    Task<IEnumerable<Models.CarrierEligibility>> GetAllAsync();
    Task<Models.CarrierEligibility?> GetByIdAsync(int id);
    Task<Models.CarrierEligibility?> GetByBusinessAndStateAsync(string business, string state);
    Task<Models.CarrierEligibility> CreateAsync(Models.CarrierEligibility eligibility);
    Task<bool> UpdateAsync(int id, Models.CarrierEligibility eligibility);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
