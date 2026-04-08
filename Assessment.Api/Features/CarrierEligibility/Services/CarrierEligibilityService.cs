using Assessment.Data;
using Dapper;

namespace Assessment.Api.Features.CarrierEligibility.Services;

public class CarrierEligibilityService : ICarrierEligibilityService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CarrierEligibilityService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Models.CarrierEligibility>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Business, State, IsEligible FROM CarrierEligibilities";
        return await connection.QueryAsync<Models.CarrierEligibility>(sql);
    }

    public async Task<Models.CarrierEligibility?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Business, State, IsEligible FROM CarrierEligibilities WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Models.CarrierEligibility>(sql, new { Id = id });
    }

    public async Task<Models.CarrierEligibility?> GetByBusinessAndStateAsync(string business, string state)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Business, State, IsEligible FROM CarrierEligibilities WHERE Business = @Business AND State = @State";
        return await connection.QuerySingleOrDefaultAsync<Models.CarrierEligibility>(sql, new { Business = business, State = state });
    }

    public async Task<Models.CarrierEligibility> CreateAsync(Models.CarrierEligibility eligibility)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            INSERT INTO CarrierEligibilities (Business, State, IsEligible)
            VALUES (@Business, @State, @IsEligible);
            SELECT last_insert_rowid();";

        var id = await connection.ExecuteScalarAsync<int>(sql, eligibility);
        eligibility.Id = id;
        return eligibility;
    }

    public async Task<bool> UpdateAsync(int id, Models.CarrierEligibility eligibility)
    {
        if (id != eligibility.Id)
        {
            return false;
        }

        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            UPDATE CarrierEligibilities
            SET Business = @Business, State = @State, IsEligible = @IsEligible
            WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, eligibility);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM CarrierEligibilities WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT COUNT(1) FROM CarrierEligibilities WHERE Id = @Id";
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return count > 0;
    }
}
