using Assessment.Api.Features.BusinessFactors.Models;
using Assessment.Data;
using Dapper;

namespace Assessment.Api.Features.BusinessFactors.Services;

public class BusinessFactorService : IBusinessFactorService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public BusinessFactorService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<BusinessFactor>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Business, Factor FROM BusinessFactors";
        return await connection.QueryAsync<BusinessFactor>(sql);
    }

    public async Task<BusinessFactor?> GetByBusinessAsync(string business)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Business, Factor FROM BusinessFactors WHERE Business = @Business";
        return await connection.QuerySingleOrDefaultAsync<BusinessFactor>(sql, new { Business = business });
    }

    public async Task<BusinessFactor> CreateAsync(BusinessFactor factor)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO BusinessFactors (Business, Factor) VALUES (@Business, @Factor)";
        await connection.ExecuteAsync(sql, factor);
        return factor;
    }

    public async Task<bool> UpdateAsync(string business, BusinessFactor factor)
    {
        if (business != factor.Business)
        {
            return false;
        }

        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE BusinessFactors SET Factor = @Factor WHERE Business = @Business";
        var rowsAffected = await connection.ExecuteAsync(sql, factor);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(string business)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM BusinessFactors WHERE Business = @Business";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Business = business });
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsAsync(string business)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT COUNT(1) FROM BusinessFactors WHERE Business = @Business";
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Business = business });
        return count > 0;
    }
}
